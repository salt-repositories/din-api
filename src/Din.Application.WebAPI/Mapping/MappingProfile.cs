using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Converters;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Application.WebAPI.Querying;
using Din.Domain.Clients.Giphy.Responses;
using Din.Domain.Clients.Unsplash.Responses;
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
            CreateMap<QueryParametersRequest, QueryParameters<AddedContent>>()
                .ConvertUsing<ToQueryParametersConverter<AddedContent>>();

            CreateMap<Giphy, GifResponse>()
                .ConvertUsing<ToGifResponseConverter>();

            CreateMap<UnsplashImage, BackgroundResponse>()
                .ConvertUsing<ToBackgroundResponseConverter>();

            CreateMap<AccountRequest, Account>();
        }
    }
}
