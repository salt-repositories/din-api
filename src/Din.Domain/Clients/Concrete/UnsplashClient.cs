using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class UnsplashClient : IUnsplashClient
    {
        private readonly HttpClient _client;
        private readonly IUnsplashClientConfig _config;

        public UnsplashClient(HttpClient httpClient, IUnsplashClientConfig config)
        {
            httpClient.BaseAddress = new Uri(config.Url);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _config = config;
        }

        public async Task<ICollection<UnsplashItem>> GetBackgroundCollection()
        {
            return new List<UnsplashItem>(JsonConvert.DeserializeObject<ICollection<UnsplashItem>>(
                await _client.GetStringAsync($"?client_id={_config.Key}&query=nature&orientation=landscape&count=20&featured")));
        }
    }
}