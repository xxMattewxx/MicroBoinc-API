using AutoMapper;
using MicroBoincAPI.Dtos.Binaries;
using MicroBoincAPI.Models.Binaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Profiles.Binaries
{
    public class ProjectBinaryProfile : Profile
    {
        public ProjectBinaryProfile()
        {
            CreateMap<ProjectBinary, ProjectBinaryReadDto>();
            CreateMap<CreateProjectBinaryDto, ProjectBinary>();
        }
    }
}
