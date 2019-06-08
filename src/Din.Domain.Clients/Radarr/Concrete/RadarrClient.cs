using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Concrete
{
    public class RadarrClient : ApiClient, IRadarrClient
    {
        private readonly IRadarrClientConfig _config;

        public RadarrClient(IHttpClientFactory clientFactory, IRadarrClientConfig config) : base(clientFactory)
        {
            _config = config;
        }

        public async Task<T> GetMoviesAsync<T>(RadarrMovieQuery query, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/movie?apikey={_config.Key}&pageSize={query.PageSize}&page={query.Page}&sortKey={query.SortKey}&sortDir={query.SortDirection}"));

            return await SendRequest<T>(request, cancellationToken);
        }

        public async Task<RadarrMovie> GetMovieByIdAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/movie/{id}?apikey={_config.Key}"));

            return await SendRequest<RadarrMovie>(request, cancellationToken);
        }

        public async Task<RadarrMovie> AddMovieAsync(RadarrMovieRequest movie, CancellationToken cancellationToken)
        {
            movie.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri($"{_config.Url}api/movie?apikey={_config.Key}"))
            {
                Content = new StringContent(JsonConvert.SerializeObject(movie))
            };

            return await SendRequest<RadarrMovie>(request, cancellationToken);
        }

        public async Task<IEnumerable<RadarrCalendar>> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken)
        {
            var (dateTime, till) = dateRange;
            var request = new HttpRequestMessage(HttpMethod.Get, 
                new Uri($"{_config.Url}api/calendar?apikey={_config.Key}&start={dateTime:yyyy-MM-dd}&end={till:yyyy-MM-dd}"));

            return await SendRequest<IEnumerable<RadarrCalendar>>(request, cancellationToken);
        }

        public async Task<IEnumerable<RadarrQueue>> GetQueueAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri($"{_config.Url}api/queue?apikey={_config.Key}"));

            return await SendRequest<IEnumerable<RadarrQueue>>(request, cancellationToken);
        }
    }
}
