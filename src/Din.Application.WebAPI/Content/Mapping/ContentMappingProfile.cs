using AutoMapper;
using Din.Application.WebAPI.Content.Responses;
using Din.Application.WebAPI.Querying;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Content.Mapping
{
    public class ContentMappingProfile : MapperConfigurationExpression
    {
        public ContentMappingProfile()
        {
            CreateMap<Domain.Models.Entities.ContentRating, ContentResponseRating>();
            CreateMap<ContentRating, ContentResponseRating>();
            CreateMap<QueryParametersRequest, QueryParameters>()
                .ConvertUsing<ToQueryParametersConverter>();
            CreateMap<ContentRating, Domain.Models.Entities.ContentRating>();
        }
    }
}
