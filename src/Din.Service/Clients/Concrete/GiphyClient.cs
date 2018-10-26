using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class GiphyClient : BaseClient, IGiphyClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGiphyClientConfig _config;

        public GiphyClient(IHttpClientFactory httpClientFactory, IGiphyClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<GiphyItem> GetRandomGifAsync(GiphyTag tag)
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<GiphyItem>(await client
                .GetStringAsync(BuildUrl(_config.Url, $"?api_key={_config.Key}", $"&tag={tag.ToString().ToLower()}", "&rating=G")));
        }
    }

    public enum GiphyTag
    {
        Nicetry,
        Bye,
        Funny,
        Trending,
        Error
    }
}