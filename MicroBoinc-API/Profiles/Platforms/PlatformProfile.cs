using AutoMapper;
using MicroBoincAPI.Dtos.Platforms;
using MicroBoincAPI.Models.Platforms;

namespace MicroBoincAPI.Profiles.Platforms
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CreatePlatformDto, Platform>();
        }
    }
}
