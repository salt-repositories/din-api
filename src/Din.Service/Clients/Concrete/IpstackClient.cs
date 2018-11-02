using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class IpStackClient : IIpStackClient
    {
        private readonly HttpClient _client;
        private readonly IIpStackClientConfig _config;

        public IpStackClient(HttpClient httpClient, IIpStackClientConfig config)
        {
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