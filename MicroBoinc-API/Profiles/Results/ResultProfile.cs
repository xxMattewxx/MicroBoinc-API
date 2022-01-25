using AutoMapper;
using MicroBoincAPI.Dtos.Results;
using MicroBoincAPI.Models.Results;

namespace MicroBoincAPI.Profiles.Results
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<SubmitResultDto, Result>();
            CreateMap<Result, SubmitResultResponseDto>();
        }
    }
}