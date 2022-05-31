using AutoMapper;
using MicroBoincAPI.Dtos.Versions;
using MicroBoincAPI.Models.Versions;

namespace MicroBoincAPI.Profiles.Versions
{
    public class VersionInfoProfile : Profile
    {
        public VersionInfoProfile()
        {
            CreateMap<VersionInfoCreateDto, VersionInfo>();
            CreateMap<VersionInfo, VersionInfoReadDto>();
        }
    }
}
