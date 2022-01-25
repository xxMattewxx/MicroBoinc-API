using AutoMapper;
using MicroBoincAPI.Data.Binaries;
using MicroBoincAPI.Data.Platforms;
using MicroBoincAPI.Data.Projects;
using MicroBoincAPI.Dtos.Binaries;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BinariesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBinariesRepo _binariesRepo;
        private readonly IPlatformsRepo _platformsRepo;
        private readonly IProjectsRepo _projectsRepo;

        public BinariesController(IMapper mapper, IBinariesRepo binariesRepo, IPlatformsRepo platformsRepo, IProjectsRepo projectsRepo)
        {
            _mapper = mapper;
            _binariesRepo = binariesRepo;
            _platformsRepo = platformsRepo;
            _projectsRepo = projectsRepo;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<ProjectBinaryReadDto> CreateProjectBinary([FromBody, Required] CreateProjectBinaryDto dto)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Only root keys can be used for adding projects binaries" });

            var platform = _platformsRepo.GetPlatformByID(dto.PlatformID.Value);
            if (platform == null)
                return NotFound(new { Message = "Platform not found" });

            var project = _projectsRepo.GetProjectByID(dto.ProjectID.Value);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var binary = new Binary
            {
                Checksum = dto.Checksum,
                DownloadURL = dto.DownloadURL
            };

            var ret = new ProjectBinary
            {
                Binary = binary,
                Project = project,
                Platform = platform,
                Priority = dto.Priority.Value
            };

            _binariesRepo.CreateProjectBinary(ret);
            _binariesRepo.SaveChanges();

            return Ok(_mapper.Map<ProjectBinaryReadDto>(ret));
        }
    }
}
