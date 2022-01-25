using AutoMapper;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Groups;
using MicroBoincAPI.Dtos.Groups;
using MicroBoincAPI.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupsRepo _repository;

        public GroupsController(IMapper mapper, IGroupsRepo repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<CreateGroupResponseDto> CreateGroup([Required, FromBody] CreateGroupDto dto)
        {
            var key = this.GetLoggedInKey();
            if (!key.IsRoot || key.IsWeak)
                return Unauthorized(new { Message = "Only root keys can be used for creating groups" });

            var group = _mapper.Map<Group>(dto);
            group.OwnedBy = key.Account;

            _repository.CreateGroup(group);
            _repository.SaveChanges();

            return _mapper.Map<CreateGroupResponseDto>(group);
        }
    }
}
