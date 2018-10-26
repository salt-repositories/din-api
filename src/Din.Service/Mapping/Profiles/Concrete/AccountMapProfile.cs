using AutoMapper;
using Din.Data.Entities;
using Din.Service.Dto.Context;
using Din.Service.Mapping.Profiles.Interfaces;

namespace Din.Service.Mapping.Profiles.Concrete
{
    public class AccountMapProfile : Profile, IAccountMapProfile
    {
        public AccountMapProfile()
        {
            CreateMap<AccountDto, AccountEntity>();
        }
    }
}
