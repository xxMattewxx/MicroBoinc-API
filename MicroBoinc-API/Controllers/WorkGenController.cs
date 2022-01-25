using System;
using AutoMapper;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Tasks;
using MicroBoincAPI.Data.Groups;
using MicroBoincAPI.Dtos.WorkGen;
using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Data.Projects;
using MicroBoincAPI.Models.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Transactions;
using MicroBoincAPI.Models.Projects;
using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Data.Assignments;
using MicroBoincAPI.Models.Assignments;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkGenController : ControllerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IGroupsRepo _groupsRepo;
        private readonly IProjectsRepo _projectsRepo;

        public WorkGenController(IServiceScopeFactory scopeFactory, IGroupsRepo groupsRepo, IProjectsRepo projectsRepo)
        {
            _scopeFactory = scopeFactory;
            _groupsRepo = groupsRepo;
            _projectsRepo = projectsRepo;
        }

        [HttpPost]
        [Authorize]
        [Route("Sequential")]
        public ActionResult<string> GenerateSequential([Required, FromBody] GenerateSequentialDto dto)
        {
            var key = this.GetLoggedInKey();
            if (key.IsWeak)
                return Unauthorized(new { Message = "Weak keys cannot be used to add tasks" });

            var project = _projectsRepo.GetProjectByID(dto.ProjectID.Value);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var perm = _groupsRepo.GetGroupPermission(project.Group.ID, key.Account.ID);
            if (!Common.IsAuthorized(key, project.Group, perm, GroupPermissionEnum.CanAddTasks))
                return Unauthorized(new { Message = "Key does not have permission to add tasks" });

            GenerateTasks(project, dto.QuorumNeeded.Value, dto.Start.Value, dto.End.Value, dto.Offset.Value);

            return Ok();
        }

        private void GenerateTasks(Project project, ushort quorumNeeded, long start, long end, long offset)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(10)
            };

            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);

            var tasks = new List<Task>();
            for (long iter = start; iter < end; iter += offset)
            {
                var task = new Task
                {
                    InputData = iter + " " + Math.Min(iter + offset, end),
                    Project = project,
                    Status = TaskStatus.Available,
                    Group = project.Group,
                    InputID = null,
                    SlotsLeft = quorumNeeded,
                    ResultsLeft = quorumNeeded
                };
                tasks.Add(task);

                if (tasks.Count == 10000)
                {
                    WriteToDatabase(project, project.Group, tasks);
                    tasks.Clear();
                }
            }

            if (tasks.Count > 0)
            {
                WriteToDatabase(project, project.Group, tasks);
                tasks.Clear();
            }

            transactionScope.Complete();
        }

        private void WriteToDatabase(Project project, Group group, List<Task> tasks)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<ITasksRepo>();

            repo.AttachEntity(group);
            repo.AttachEntity(project);

            repo.CreateTasks(tasks);
            repo.SaveChanges();
            scope.Dispose();
        }
    }
}
