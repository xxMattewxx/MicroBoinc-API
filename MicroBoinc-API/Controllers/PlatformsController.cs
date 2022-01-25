using AutoMapper;
using MicroBoincAPI.Data.Platforms;
using MicroBoincAPI.Dtos.Platforms;
using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlatformsRepo _repository;

        public PlatformsController(IMapper mapper, IPlatformsRepo repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public ActionResult<PlatformReadDto> CreatePlatform(CreatePlatformDto dto)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Platforms can only be added by root keys" });

            var platform = _mapper.Map<Platform>(dto);
            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpGet]
        [Authorize]
        [Route("List")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }
    }
}
