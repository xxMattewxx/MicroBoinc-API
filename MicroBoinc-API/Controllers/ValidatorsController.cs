using MicroBoincAPI.Utils;
using MicroBoincAPI.Data.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Dtos.Validators;
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
    public class ValidatorsController : ControllerBase
    {
        private readonly IAssignmentsRepo _assignmentsRepo;
        private readonly ILeaderboardsRepo _leaderboardsRepo;
        private readonly ITasksRepo _tasksRepo;

        public ValidatorsController(IAssignmentsRepo assignmentsRepo, ILeaderboardsRepo leaderboardsRepo, ITasksRepo tasksRepo)
        {
            _assignmentsRepo = assignmentsRepo;
            _leaderboardsRepo = leaderboardsRepo;
            _tasksRepo = tasksRepo;
        }

        [HttpPost]
        [Authorize]
        [Route("Apply")]
        public ActionResult<string> ApplyValidationData([Required, FromBody] ApplyValidationDataDto dto)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Key is not root" });

            do
            {
                try
                {
                    OptimisticApplyValidationData(dto);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _assignmentsRepo.ResetContext();
                    _leaderboardsRepo.ResetContext();
                    _tasksRepo.ResetContext();

                    _leaderboardsRepo.AttachEntity(key);
                }
            }
            while (true);
        }

        private void OptimisticApplyValidationData(ApplyValidationDataDto dto)
        {
            foreach (var id in dto.ApprovedAssignmentIDs)
            {
                var assignment = _assignmentsRepo.GetAssignmentByID(id);
                assignment.Status = AssignmentStatus.Validated;
                _leaderboardsRepo.AddValidPointsToAccount(assignment.AssignedTo, assignment.Task.Project);
            }

            foreach (var id in dto.RefusedAssignmentIDs)
            {
                var assignment = _assignmentsRepo.GetAssignmentByID(id);
                assignment.Status = AssignmentStatus.Invalidated;
                _leaderboardsRepo.AddInvalidPointsToAccount(assignment.AssignedTo, assignment.Task.Project);
            }

            foreach (var id in dto.TaskIDsToValidate)
                _tasksRepo.UpdateStatus(id, TaskStatus.Completed);

            foreach (var id in dto.TaskIDsToRegenerate)
                _tasksRepo.IncreaseTaskSlots(id);

            _leaderboardsRepo.SaveChanges();
            _assignmentsRepo.SaveChanges();
            _tasksRepo.SaveChanges();
        }
    }
}
