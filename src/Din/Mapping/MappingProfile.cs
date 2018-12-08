using AutoMapper.Configuration;
using Din.Data.Entities;
using Din.Requests;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dtos;
using Din.Service.Mapping.Converters;

namespace Din.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<AccountRequest, AccountEntity>().ReverseMap();
            CreateMap<McCalendar, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendar, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
