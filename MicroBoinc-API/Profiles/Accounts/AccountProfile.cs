using AutoMapper;
using MicroBoincAPI.Dtos.Accounts;
using MicroBoincAPI.Models.Accounts;

namespace MicroBoincAPI.Profiles.Accounts
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountReadDto>();
            CreateMap<CreateAccountDto, Account>();
        }
    }
}
