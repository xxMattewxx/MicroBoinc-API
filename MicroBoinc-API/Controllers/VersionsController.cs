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
using MicroBoincAPI.Data.Versions;
using MicroBoincAPI.Dtos.Versions;
using MicroBoincAPI.Data.Binaries;
using MicroBoincAPI.Models.Versions;

//TODO BENCHMARK OPTIMISTIC VS PESSIMISTIC TASK RETRIEVAL
namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBinariesRepo _binariesRepo;
        private readonly IVersionsRepo _versionsRepo;

        public VersionsController(IMapper mapper, IBinariesRepo binariesRepo, IVersionsRepo versionsRepo)
        {
            _mapper = mapper;
            _binariesRepo = binariesRepo;
            _versionsRepo = versionsRepo;
        }

        [HttpGet("Latest")]
        public ActionResult<VersionInfoReadDto> GetLatest()
        {
            var version = _versionsRepo.GetLatest();
            return Ok(_mapper.Map<VersionInfoReadDto>(version));
        }
    }
}
