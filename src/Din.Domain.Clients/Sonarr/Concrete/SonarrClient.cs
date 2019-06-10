using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Concrete
{
    public class SonarrClient : ApiClient, ISonarrClient
    {
        private readonly ISonarrClientConfig _config;

        public SonarrClient(IHttpClientFactory clientFactory, ISonarrClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public async Task<IEnumerable<SonarrTvShow>> GetTvShowsAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/series?apikey={_config.Key}"));

            return await SendRequest<IEnumerable<SonarrTvShow>>(request, cancellationToken);
        }

        public async Task<SonarrTvShow> GetTvShowByIdAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/series/{id}?apikey={_config.Key}"));

            return await SendRequest<SonarrTvShow>(request, cancellationToken);
        }

        public async Task<SonarrTvShow> AddTvShowAsync(SonarrTvShowRequest tvShow, CancellationToken cancellationToken)
        {
            tvShow.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri($"{_config.Url}api/series?apikey={_config.Key}"))
            {
                Content = new StringContent(JsonConvert.SerializeObject(tvShow))
            };

            return await SendRequest<SonarrTvShow>(request, cancellationToken);
        }

        public async Task<IEnumerable<SonarrTvShow>> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken)
        {
            var (from, till) = dateRange;

            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/calendar?apikey={_config.Key}&start={from:yyyy-MM-dd}&end={till:yyyy-MM-dd}"));

            return await SendRequest<IEnumerable<SonarrTvShow>>(request, cancellationToken);
        }

        public async Task<IEnumerable<SonarrQueue>> GetQueueAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/queue?apikey={_config.Key}"));

            return await SendRequest<IEnumerable<SonarrQueue>>(request, cancellationToken);
        }
    }
}
