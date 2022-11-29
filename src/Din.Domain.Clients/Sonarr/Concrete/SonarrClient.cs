using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Domain.Clients.Sonarr.Concrete
{
    public class SonarrClient : ApiClient, ISonarrClient
    {
        private readonly ISonarrClientConfig _config;

        public SonarrClient(IHttpClientFactory clientFactory, ISonarrClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public Task<IEnumerable<SonarrTvShow>> GetTvShowsAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri($"{_config.Url}api/v3/series?apikey={_config.Key}"));

            return SendRequest<IEnumerable<SonarrTvShow>>(request, cancellationToken);
        }

        public Task<SonarrTvShow> GetTvShowByIdAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri($"{_config.Url}api/v3/series/{id}?apikey={_config.Key}"));

            return SendRequest<SonarrTvShow>(request, cancellationToken);
        }

        public Task<SonarrTvShow> AddTvShowAsync(SonarrTvShowRequest tvShow, CancellationToken cancellationToken)
        {
            tvShow.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri($"{_config.Url}api/v3/series?apikey={_config.Key}"))
            {
                Content = JsonContent(tvShow)
            };

            return SendRequest<SonarrTvShow>(request, cancellationToken);
        }

        public Task<IEnumerable<SonarrCalendar>> GetCalendarAsync((DateTime from, DateTime till) dateRange,
            CancellationToken cancellationToken)
        {
            var (from, till) = dateRange;

            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri(
                    $"{_config.Url}api/v3/calendar?apikey={_config.Key}&start={from:yyyy-MM-dd}&end={till:yyyy-MM-dd}"));

            return SendRequest<IEnumerable<SonarrCalendar>>(request, cancellationToken);
        }

        public Task<IEnumerable<SonarrQueue>> GetQueueAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri($"{_config.Url}api/v3/queue?apikey={_config.Key}"));

            return SendRequest<IEnumerable<SonarrQueue>>(request, cancellationToken);
        }

        public Task<IEnumerable<SonarrEpisode>> GetTvShowEpisodes(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/v3/episode?seriesId={id}&apikey={_config.Key}"));

            return SendRequest<IEnumerable<SonarrEpisode>>(request, cancellationToken);
        }
    }
}