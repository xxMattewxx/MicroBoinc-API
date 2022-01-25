using AutoMapper;
using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Profiles.Projects
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<Project, CreateProjectResponseDto>();
        }
    }
}
