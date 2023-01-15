using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Application.WebAPI.Media.Responses
{
    public record struct BackgroundResponse
    {
        public string Raw { get; init; }
        public string Full { get; init; }
        public string Regular { get; init; }
        public string Small { get; init; }
        public string Thumb { get; init; }

        public static implicit operator BackgroundResponse(UnsplashImage unsplashImage) => new()
        {
            Raw = unsplashImage.Urls.Raw,
            Full = unsplashImage.Urls.Full,
            Regular = unsplashImage.Urls.Regular,
            Small = unsplashImage.Urls.Small,
            Thumb = unsplashImage.Urls.Thumb
        };
    }
}