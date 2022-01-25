//THIS SHOULD BE MOVED TO ITS OWN MICROSERVICE SOON.

using AutoMapper;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Results;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using MicroBoincAPI.Dtos.Results;
using MicroBoincAPI.Models.Results;
using MicroBoincAPI.Data.Projects;
using MicroBoincAPI.Data.Assignments;
using MicroBoincAPI.Models.Assignments;
using MicroBoincAPI.Models.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using MicroBoincAPI.Data.Leaderboards;
using System;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentsRepo _assignmentsRepo;
        private readonly IResultsRepo _repository;
        private readonly ILeaderboardsRepo _leaderboardsRepo;

        public ResultsController(IMapper mapper, IAssignmentsRepo assignmentsRepo, IResultsRepo repository, ILeaderboardsRepo leaderboardsRepo)
        {
            _mapper = mapper;
            _assignmentsRepo = assignmentsRepo;
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
                catch (DbUpdateConcurrencyException) {
                    _repository.ResetContext();
                    _assignmentsRepo.ResetContext();
                    _leaderboardsRepo.ResetContext();
                }
            }
            while (true);
        }

        private Result ProcessResult(SubmitResultDto dto)
        {
            var assignment = _assignmentsRepo.GetAssignmentByID(dto.AssignmentID.Value);
            var result = _mapper.Map<Result>(dto);
            result.Task = assignment.Task;
            result.Assignment = assignment;
            result.Project = assignment.Task.Project;

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
