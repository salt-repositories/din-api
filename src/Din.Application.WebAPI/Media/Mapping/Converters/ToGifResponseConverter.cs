using AutoMapper;
using Din.Application.WebAPI.Media.Responses;
using Din.Domain.Clients.Giphy.Responses;

namespace Din.Application.WebAPI.Media.Mapping.Converters
{
    public class ToGifResponseConverter : ITypeConverter<Giphy, GifResponse>
    {
        public GifResponse Convert(Giphy source, GifResponse destination, ResolutionContext context)
        {
            return new GifResponse
            {
                Title = source.Data.Title,
                Url = source.Data.Url,
                Mp4Url = source.Data.Mp4Url,
                Frames = source.Data.Frames,
                Height = source.Data.Height,
                Width = source.Data.Width
            };
        }
    }
}
