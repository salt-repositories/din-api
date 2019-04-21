using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class IpStackClient : IIpStackClient
    {
        private readonly HttpClient _client;
        private readonly IIpStackClientConfig _config;

        public IpStackClient(IHttpClientFactory clientFactory, IIpStackClientConfig config)
        {
            var httpClient = clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(config.Url);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _config = config;
        }

        public async Task<IpStackLocation> GetLocation(string ip)
        {
            return JsonConvert.DeserializeObject<IpStackLocation>(await _client.GetStringAsync($"{ip}?access_key={_config.Key}"));
        }
    }
}