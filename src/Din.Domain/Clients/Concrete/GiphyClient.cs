using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class GiphyClient : IGiphyClient
    {
        private readonly HttpClient _client;
        private readonly IGiphyClientConfig _config;

        public GiphyClient(HttpClient httpClient, IGiphyClientConfig config)
        {
            httpClient.BaseAddress = new Uri(config.Url);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _config = config;
        }

        public async Task<GiphyItem> GetRandomGifAsync(string query) 
        {
            return JsonConvert.DeserializeObject<GiphyItem>(await _client
                .GetStringAsync($"?api_key={_config.Key}&tag={query}&rating=G"));
        }
    }
}