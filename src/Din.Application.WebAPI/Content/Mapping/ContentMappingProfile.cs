using AutoMapper.Configuration;
using Din.Application.WebAPI.Content.Responses;
using Din.Domain.Clients.Abstractions;

namespace Din.Application.WebAPI.Content.Mapping
{
    public class ContentMappingProfile : MapperConfigurationExpression
    {
        public ContentMappingProfile()
        {
            CreateMap<ContentRating, ContentResponseRating>();
        }
    }
}
