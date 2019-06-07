using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Giphy.Interfaces;
using Din.Domain.Clients.Giphy.Response;

namespace Din.Domain.Clients.Giphy.Concrete
{
    public class GiphyClient : ApiClient, IGiphyClient
    {
        private readonly IGiphyClientConfig _config;
        private const string ApiVersion = "v1";

        public GiphyClient(IHttpClientFactory clientFactory, IGiphyClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public async Task<GiphyResponse> GetRandomGifAsync(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}{ApiVersion}/gif/random?api_key={_config.Key}&tag={query}"));

            return await SendRequest<GiphyResponse>(request);
        }
    }
}
