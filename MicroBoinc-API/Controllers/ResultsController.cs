//THIS SHOULD BE MOVED TO ITS OWN MICROSERVICE SOON.

using System.IO;
using AutoMapper;
using System.Transactions;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Tasks;
using System.Collections.Generic;
using MicroBoincAPI.Data.Results;
using MicroBoincAPI.Dtos.Results;
using MicroBoincAPI.Models.Results;
using Microsoft.EntityFrameworkCore;
using MicroBoincAPI.Data.Assignments;
using MicroBoincAPI.Data.Leaderboards;
using MicroBoincAPI.Models.Assignments;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentsRepo _assignmentsRepo;
        private readonly ITasksRepo _tasksRepo;
        private readonly IResultsRepo _repository;
        private readonly ILeaderboardsRepo _leaderboardsRepo;

        public ResultsController(IMapper mapper, IAssignmentsRepo assignmentsRepo, ITasksRepo tasksRepo, IResultsRepo repository, ILeaderboardsRepo leaderboardsRepo)
        {
            _mapper = mapper;
            _assignmentsRepo = assignmentsRepo;
            _tasksRepo = tasksRepo;
            _repository = repository;
            _leaderboardsRepo = leaderboardsRepo;
        }

        [HttpPost]
        [Authorize]
        [Route("Submit")]
        public ActionResult<SubmitResultResponseDto> SubmitResult([Required, FromBody] SubmitResultDto dto)
        {
            var key = this.GetLoggedInKey();

            var assignment = _assignmentsRepo.GetAssignmentByID(dto.AssignmentID.Value);
            if (assignment == null)
                return NotFound(new { Message = "Assignment not found" });

            if (assignment.AssignedToID != key.ID)
                return Unauthorized(new { Message = "Not owner of assignment" });

            if (assignment.Status != AssignmentStatus.Sent)
                return Conflict(new { Message = "Assignment is not in a valid state for update" });

            do
            {
                try
                {
                    var ret = ProcessResult(dto);
                    return Ok(_mapper.Map<SubmitResultResponseDto>(ret));
                }
                catch (DbUpdateException) {
                    _repository.ResetContext();
                    _assignmentsRepo.ResetContext();
                    _leaderboardsRepo.ResetContext();
                }
            }
            while (true);
        }

        [HttpGet]
        [Authorize]
        [Route("ByTaskID/{taskID}")]
        public ActionResult<IEnumerable<ResultReadDto>> GetResultsForTask(long taskID)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Key must be root" });

            var results = _repository.GetResultsForTask(taskID);
            return Ok(_mapper.Map<IEnumerable<ResultReadDto>>(results));
        }

        [HttpGet]
        [Authorize]
        [Route("ByProjectID/{projectID}")]
        public void GetResultsForProject(long projectID)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return;

            using var writer = new StreamWriter(Response.Body);
            _repository.StreamResults(projectID, writer);
        }

        private Result ProcessResult(SubmitResultDto dto)
        {
            var assignment = _assignmentsRepo.GetAssignmentByID(dto.AssignmentID.Value);
            var result = _mapper.Map<Result>(dto);
            result.Task = assignment.Task;
            result.Assignment = assignment;
            result.Project = assignment.Task.Project;

            _tasksRepo.DecreaseResultsLeft(assignment.TaskID);
            _repository.CreateResult(result);
            _leaderboardsRepo.AddPointsToAccount(assignment.AssignedTo, result.Project);
            _assignmentsRepo.UpdateStatus(result.Assignment, AssignmentStatus.Received);

            using (var scope = new TransactionScope())
            {
                _repository.SaveChanges();
                _assignmentsRepo.SaveChanges();
                _leaderboardsRepo.SaveChanges();

                scope.Complete();
            }
            return result;
        }
    }
}
