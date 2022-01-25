using AutoMapper;
using MicroBoincAPI.Dtos.Tasks;
using MicroBoincAPI.Models.Tasks;

namespace MicroBoincAPI.Profiles.Tasks
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskReadDto>();
        }
    }
}
