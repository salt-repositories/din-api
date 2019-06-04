using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Converters;
using Din.Application.WebAPI.Querying;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<QueryParametersRequest, QueryParameters<Account>>()
                .ConvertUsing<ToQueryParametersConverter<Account>>();
            CreateMap<McCalendar, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendar, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
