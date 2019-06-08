using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Clients.IpStack.Responses;

namespace Din.Domain.Clients.IpStack.Concrete
{
    public class IpStackClient : ApiClient, IIpStackClient
    {
        private readonly IIpStackClientConfig _config;

        public IpStackClient(IHttpClientFactory clientFactory, IIpStackClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public async Task<IpStackLocation> GetLocationAsync(string ip, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}{ip}?access_key={_config.Key}"));

            return await SendRequest<IpStackLocation>(request, cancellationToken);
        }
    }
}
