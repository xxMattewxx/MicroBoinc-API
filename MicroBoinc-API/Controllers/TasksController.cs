using AutoMapper;
using MicroBoincAPI.Data.Tasks;
using MicroBoincAPI.Dtos.Tasks;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRepo _repository;

        public TasksController(ITasksRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        [Route("ToValidate/{projectID}")]
        public ActionResult<GetTasksToValidateResponseDto> GetTasksToValidate([Required] long? projectID)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Key must be root" });

            return Ok(new GetTasksToValidateResponseDto
            {
                TaskIDs = _repository.GetTaskIDsToValidate(projectID.Value)
            });
        }
    }
}
