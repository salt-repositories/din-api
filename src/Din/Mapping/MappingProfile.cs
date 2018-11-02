using AutoMapper.Configuration;
using Din.Data.Entities;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Context;
using Din.Service.DTO.Content;
using Din.Service.Mapping.Converters;
using Microsoft.AspNetCore.JsonPatch;

namespace Din.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<AccountEntity, AccountDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<McCalendar, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendar, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
            CreateMap<JsonPatchDocument<AccountDto>, JsonPatchDocument<AccountEntity>>();
        }
    }
}
