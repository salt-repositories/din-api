using AutoMapper.Configuration;
using Din.Application.WebAPI.Media.Mapping.Converters;
using Din.Application.WebAPI.Media.Responses;
using Din.Domain.Clients.Giphy.Responses;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Application.WebAPI.Media.Mapping
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
