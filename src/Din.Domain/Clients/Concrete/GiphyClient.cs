using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Din.Domain.Exceptions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class GiphyClient : IGiphyClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IGiphyClientConfig _config;

        public GiphyClient(IHttpClientFactory clientFactory, IGiphyClientConfig config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<GiphyItem> GetRandomGifAsync(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}?api_key={_config.Key}&tag={query}&rating=G"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET random gif failed");
                }


                return JsonConvert.DeserializeObject<GiphyItem>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}