using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Giphy.Interfaces;

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

        public Task<Responses.Giphy> GetRandomGifByTagAsync(string tag, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}{ApiVersion}/gifs/random?api_key={_config.Key}&tag={tag}"));

            return SendRequest<Responses.Giphy>(request, cancellationToken);
        }
    }
}
