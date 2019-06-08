using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Configuration.Interfaces;
using Din.Domain.Clients.Unsplash.Interfaces;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Domain.Clients.Unsplash.Concrete
{
    public class UnsplashClient : ApiClient, IUnsplashClient
    {
        private readonly IUnsplashClientConfig _config;

        public UnsplashClient(IHttpClientFactory clientFactory, IUnsplashClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public async Task<IEnumerable<UnsplashImage>> GetImagesAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}photos/random?client_id={_config.Key}&query=nature&orientation=landscape&count=20&featured"));

            return await SendRequest<IEnumerable<UnsplashImage>>(request, cancellationToken);
        }
    }
}
