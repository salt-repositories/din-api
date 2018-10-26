using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Din.Service.Dto.Context;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class IpStackClient : BaseClient, IIpStackClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIpStackClientConfig _config;

        public IpStackClient(IHttpClientFactory httpClientFactory, IIpStackClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IpStackLocation> GetLocation(string ip)
        {
            var client = _httpClientFactory.CreateClient();
            return JsonConvert.DeserializeObject<IpStackLocation>(await client.GetStringAsync(BuildUrl(_config.Url, ip, $"?access_key={_config.Key}")));
        }
    }
}