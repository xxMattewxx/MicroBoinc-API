using System;
using AutoMapper;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Tasks;
using MicroBoincAPI.Dtos.Feeder;
using MicroBoincAPI.Models.Assignments;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MicroBoincAPI.Dtos.Assignments;
using MicroBoincAPI.Data.Assignments;
using System.Linq;
using MicroBoincAPI.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

//TODO BENCHMARK OPTIMISTIC VS PESSIMISTIC TASK RETRIEVAL
namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeederController : ControllerBase
    {
        private static readonly Random rnd = new();
        private readonly IMapper _mapper;
        private readonly IAssignmentsRepo _assignmentsRepo;
        private readonly ITasksRepo _tasksRepo;

        public FeederController(IMapper mapper, IAssignmentsRepo assignmentsRepo, ITasksRepo tasksRepo)
        {
            _mapper = mapper;
            _assignmentsRepo = assignmentsRepo;
            _tasksRepo = tasksRepo;
        }

        [HttpGet, HttpPost]
        [Authorize]
        [Route("OfGroups")]
        public ActionResult<RetrieveTaskResponseDto> RetrieveTasksOfGroups([FromBody, Required] RetrieveTaskOfGroupsDto dto)
        {
            if (dto.TaskCount.Value < 1 || dto.TaskCount.Value > 128)
                return BadRequest(new { Message = "TaskCount must be a valid value from 1 to 128" });

            var key = this.GetLoggedInKey();
            do
            {
                try
                {
                    var assignments = OptimisticRetrieveTasksByGroups(key, dto.AcceptedGroupsIDs, dto.TaskCount.Value);

                    return Ok(new RetrieveTaskResponseDto
                    {
                        Assignments = _mapper.Map<IEnumerable<AssignmentReadDto>>(assignments)
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    _assignmentsRepo.ResetContext();
                    _assignmentsRepo.AttachEntity(key);
                }
                Thread.Sleep(rnd.Next(20, 40));
            }
            while (true);
        }

        [HttpGet, HttpPost]
        [Authorize]
        [Route("OfProjects")]
        public ActionResult<RetrieveTaskResponseDto> RetrieveTasksOfProjects([FromBody, Required] RetrieveTaskOfProjectsDto dto)
        {
            if (dto.TaskCount.Value < 1 || dto.TaskCount.Value > 128)
                return BadRequest(new { Message = "TaskCount must be a valid value from 1 to 128" });

            var key = this.GetLoggedInKey();
            do
            {
                try
                {
                    var assignments = OptimisticRetrieveTasksByProjects(key, dto.AcceptedProjectsIDs, dto.TaskCount.Value);

                    return Ok(new RetrieveTaskResponseDto
                    {
                        Assignments = _mapper.Map<IEnumerable<AssignmentReadDto>>(assignments)
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    _assignmentsRepo.ResetContext();
                    _assignmentsRepo.AttachEntity(key);
                }
                Thread.Sleep(rnd.Next(20, 40));
            }
            while (true);
        }

        private List<Assignment> OptimisticRetrieveTasksByGroups(AccountKey key, IEnumerable<long> acceptedGroupsIDs, int count)
        {
            var tasks = _tasksRepo.RetrieveTaskOfGroups(key, acceptedGroupsIDs, count);
            if (tasks == null || !tasks.Any())
                return null;

            var assignments = new List<Assignment>();
            foreach (var task in tasks)
            {
                var assignment = new Assignment
                {
                    Task = task,
                    AssignedTo = key,
                    Status = AssignmentStatus.Sent,
                    Deadline = DateTime.Now.AddHours(2)
                };

                task.SlotsLeft--;
                _assignmentsRepo.CreateAssignment(assignment);
                assignments.Add(assignment);

                if (task.ID > key.Account.Offset)
                    key.Account.Offset = assignment.Task.ID;
            }

            _assignmentsRepo.SaveChanges();
            return assignments;
        }

        private List<Assignment> OptimisticRetrieveTasksByProjects(AccountKey key, IEnumerable<long> acceptedProjectsIDs, int count)
        {
            var tasks = _tasksRepo.RetrieveTaskOfProjects(key, acceptedProjectsIDs, count);
            if (tasks == null || !tasks.Any())
                return null;

            var assignments = new List<Assignment>();
            foreach (var task in tasks)
            {
                var assignment = new Assignment
                {
                    Task = task,
                    AssignedTo = key,
                    Status = AssignmentStatus.Sent,
                    Deadline = DateTime.Now.AddHours(2)
                };

                task.SlotsLeft--;
                _assignmentsRepo.CreateAssignment(assignment);
                assignments.Add(assignment);

                if (task.ID > key.Account.Offset)
                    key.Account.Offset = assignment.Task.ID;
            }

            _assignmentsRepo.SaveChanges();
            return assignments;
        }
    }
}
