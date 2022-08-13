﻿using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Clients.Plex.Responses;

namespace Din.Domain.Clients.Plex.Concrete
{
    public class PlexClient : ApiClient, IPlexClient
    {
        private readonly IPlexClientConfig _config;

        public PlexClient(IHttpClientFactory clientFactory, IPlexClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public Task<SearchResponse> SearchByTitle(string title, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri($"{_config.Url}search?query={UrlEncoder.Default.Encode(title)}&X-Plex-Token={_config.Key}")    
            );

            return SendRequest<SearchResponse>(request, cancellationToken);
        }
    }
}
