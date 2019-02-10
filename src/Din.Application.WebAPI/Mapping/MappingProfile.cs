using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Converters;
using Din.Application.WebAPI.Requests;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entity;

namespace Din.Application.WebAPI.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<AccountRequest, Account>().ReverseMap();
            CreateMap<McCalendar, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendar, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
