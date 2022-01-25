using AutoMapper;
using MicroBoincAPI.Dtos.Groups;
using MicroBoincAPI.Models.Groups;

namespace MicroBoincAPI.Profiles.Groups
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<CreateGroupDto, Group>();
            CreateMap<Group, CreateGroupResponseDto>();
        }
    }
}
