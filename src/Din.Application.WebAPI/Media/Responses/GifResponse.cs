using Din.Domain.Clients.Giphy.Responses;

namespace Din.Application.WebAPI.Media.Responses
{
    public record struct GifResponse
    { 
        public string Title { get; init; }
        public string Url { get; init; }
        public string Mp4Url { get; init; }
        public int Frames { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        public static implicit operator GifResponse(Giphy giphy) => new()
        {
            Title = giphy.Data.Title,
            Url = giphy.Data.Url,
            Mp4Url = giphy.Data.Mp4Url,
            Frames = giphy.Data.Frames,
            Width = giphy.Data.Width,
            Height = giphy.Data.Height
        };
    }
}