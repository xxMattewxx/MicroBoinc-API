using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Projects;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using MicroBoincAPI.Utils;
using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Projects;
using System;
using MicroBoincAPI.Data.Groups;
using MicroBoincAPI.Models.Permissions;
using System.Collections.Generic;
using MicroBoincAPI.Dtos.Binaries;
using MicroBoincAPI.Data.Binaries;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBinariesRepo _binariesRepo;
        private readonly IGroupsRepo _groupsRepo;
        private readonly IProjectsRepo _repository;

        public ProjectsController(IMapper mapper, IBinariesRepo binariesRepo, IGroupsRepo groupsRepo, IProjectsRepo repository)
        {
            _mapper = mapper;
            _binariesRepo = binariesRepo;
            _groupsRepo = groupsRepo;
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<CreateProjectResponseDto> CreateProject([Required, FromBody] CreateProjectDto dto)
        {
            var key = this.GetLoggedInKey();
            if (key.IsWeak)
                return Unauthorized(new { Message = "Weak account keys can't be used to create projects" });

            var group = _groupsRepo.GetGroupByID(dto.GroupID.Value);
            var perm = _groupsRepo.GetGroupPermission(dto.GroupID.Value, key.Account.ID);

            if (!Common.IsAuthorized(key, group, perm, GroupPermissionEnum.CanCreateProjects))
                return Unauthorized(new { Message = "Missing needed permission" });

            var project = _mapper.Map<Project>(dto);

            project.AddedAt = DateTime.Now;
            project.AddedBy = key.Account;
            project.Group = group;

            _repository.CreateProject(project);
            _repository.SaveChanges();

            return _mapper.Map<CreateProjectResponseDto>(project);
        }

        [HttpGet, HttpPost]
        [Authorize]
        [Route("Compatible")]
        public ActionResult<GetProjectsForPlatformsResponseDto> GetProjectsForPlatforms([Required, FromBody] GetProjectsForPlatformsDto dto)
        {
            var ret = _binariesRepo.GetProjectsBinariesForPlatforms(dto.PlatformsIDs);
            return Ok(ret);
        }

        [HttpGet("{projectID}")]
        [Authorize]
        public ActionResult<ProjectReadDto> GetProject(long projectID)
        {
            var project = _repository.GetProjectByID(projectID);
            return Ok(_mapper.Map<ProjectReadDto>(project));
        }

        [HttpGet("{projectID}/Progress")]
        [Authorize]
        public ActionResult<double> GetProgress(long projectID)
        {
            var project = _repository.GetProjectByID(projectID);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var (totalDone, totalGenerated) = _repository.GetProjectProgress(projectID);
            return Ok(new GetProgressDto
            {
                Name = project.Name,
                TotalDone = totalDone,
                TotalGenerated = totalGenerated
            });
        }
    }
}
