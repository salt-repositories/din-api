using AutoMapper;
using Din.Application.WebAPI.Models.Response;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Application.WebAPI.Mapping.Converters
{
    public class ToBackgroundResponseConverter : ITypeConverter<UnsplashImage, BackgroundResponse>
    {
        public BackgroundResponse Convert(UnsplashImage source, BackgroundResponse destination, ResolutionContext context)
        {
            return new BackgroundResponse
            {
                Raw = source.Urls.Raw,
                Full = source.Urls.Full,
                Regular = source.Urls.Regular,
                Small = source.Urls.Small,
                Thumb = source.Urls.Thumb
            };
        }
    }
}
