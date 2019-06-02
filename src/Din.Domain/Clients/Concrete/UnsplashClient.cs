using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Configurations.Interfaces;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Exceptions;
using Din.Domain.Exceptions.Concrete;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class UnsplashClient : IUnsplashClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IUnsplashClientConfig _config;

        public UnsplashClient(IHttpClientFactory clientFactory, IUnsplashClientConfig config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<ICollection<UnsplashItem>> GetBackgroundCollection()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}?client_id={_config.Key}&query=nature&orientation=landscape&count=20&featured"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET calender by date range failed");
                }

                return JsonConvert.DeserializeObject<ICollection<UnsplashItem>>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}