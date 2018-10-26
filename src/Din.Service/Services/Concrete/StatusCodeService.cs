using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Dto;
using Din.Service.Services.Interfaces;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc />
    public class StatusCodeService : IStatusCodeService
    {
        private readonly IGiphyClient _client;

        public StatusCodeService(IGiphyClient client)
        {
            _client = client;
        }

        public async Task<StatusCodeDto> GenerateDataToDisplayAsync(int statusCode)
        {
            var Dto = new StatusCodeDto
            {
                StatusCode = statusCode
            };
            switch (statusCode)
            {
                case 400:
                    Dto.StatusMessage = "Te Fck did you do";
                    Dto.Gif =  await _client.GetRandomGifAsync(GiphyTag.Trending);
                    break;
                case 401:
                    Dto.StatusMessage = "You're not supposed to do that";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Nicetry); 
                    break;
                case 403:
                    Dto.StatusMessage = "No No No";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Nicetry);
                    break;
                case 404:
                    Dto.StatusMessage = "It's gone";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Funny);
                    break;
                case 408:
                    Dto.StatusMessage = "The server timed out waiting for the request";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
                case 500:
                    Dto.StatusMessage = "Hmmm seems like I fucked up";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
                default:
                    Dto.StatusMessage = "Hmmm seems like I fucked up";
                    Dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
            }
            return Dto;
        }
    }
}
