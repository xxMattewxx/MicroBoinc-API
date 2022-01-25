using AutoMapper;
using MicroBoincAPI.Dtos.Assignments;
using MicroBoincAPI.Dtos.Feeder;
using MicroBoincAPI.Models.Assignments;

namespace MicroBoincAPI.Profiles.Assignments
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentReadDto>();
        }
    }
}
