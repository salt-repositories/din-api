using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
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