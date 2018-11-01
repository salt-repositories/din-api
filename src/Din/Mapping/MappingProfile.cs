using AutoMapper.Configuration;
using Din.Data.Entities;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Context;
using Din.Service.DTO.Content;
using Din.Service.Mapping.Converters;

namespace Din.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, AccountEntity>();
            CreateMap<UserDto, UserEntity>();
            CreateMap<McCalendar, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendar, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
