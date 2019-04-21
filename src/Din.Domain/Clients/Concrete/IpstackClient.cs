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
    public class IpStackClient : IIpStackClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIpStackClientConfig _config;

        public IpStackClient(IHttpClientFactory clientFactory, IIpStackClientConfig config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<IpStackLocation> GetLocation(string ip)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}{ip}?access_key={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET location failed");
                }

                return JsonConvert.DeserializeObject<IpStackLocation>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}