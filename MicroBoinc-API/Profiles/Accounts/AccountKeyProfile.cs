using AutoMapper;
using MicroBoincAPI.Dtos.Accounts;
using MicroBoincAPI.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Profiles.Accounts
{
    public class AccountKeyProfile : Profile
    {
        public AccountKeyProfile()
        {
            CreateMap<AccountKey, CreateAccountResponseDto>()
                .ForMember(x => x.ID, opt => opt.MapFrom(src => src.Account.ID))
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Identifier));
        }
    }
}
