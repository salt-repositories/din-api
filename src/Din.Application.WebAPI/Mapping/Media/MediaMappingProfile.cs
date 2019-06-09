using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Media.Converters;
using Din.Application.WebAPI.Models.Response;
using Din.Domain.Clients.Giphy.Responses;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Application.WebAPI.Mapping.Media
{
    public class MediaMappingProfile : MapperConfigurationExpression
    {
        public MediaMappingProfile()
        {
            CreateMap<Giphy, GifResponse>()
                .ConvertUsing<ToGifResponseConverter>();

            CreateMap<UnsplashImage, BackgroundResponse>()
                .ConvertUsing<ToBackgroundResponseConverter>();
        }
    }
}
