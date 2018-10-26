using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class UnsplashClient : BaseClient, IUnsplashClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnsplashClientConfig _config;

        public UnsplashClient(IHttpClientFactory httpClientFactory, IUnsplashClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<ICollection<UnsplashItem>> GetBackgroundCollection()
        {
            var client = _httpClientFactory.CreateClient();

            return new List<UnsplashItem>(JsonConvert.DeserializeObject<ICollection<UnsplashItem>>(
                await client.GetStringAsync(BuildUrl(_config.Url, $"?client_id={_config.Key}", "&query=nature&orientation=landscape&count=20&featured"))));
        }
    }
}