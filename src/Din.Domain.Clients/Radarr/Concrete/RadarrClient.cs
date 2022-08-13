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

        public Task<IEnumerable<RadarrMovie>> GetMoviesAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            (
                HttpMethod.Get, 
                new Uri($"{_config.Url}api/v3/movie?apikey={_config.Key}")
            );

            return SendRequest<IEnumerable<RadarrMovie>>(request, cancellationToken);
        }

        public Task<RadarrMovie> GetMovieByIdAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            (
                HttpMethod.Get, 
                new Uri($"{_config.Url}api/v3/movie/{id}?apikey={_config.Key}")
            );

            return SendRequest<RadarrMovie>(request, cancellationToken);
        }

        public Task<RadarrMovie> AddMovieAsync(RadarrMovieRequest movie, CancellationToken cancellationToken)
        {
            movie.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage
            (
                HttpMethod.Post,
                new Uri($"{_config.Url}api/v3/movie?apikey={_config.Key}")
            )
            {
                Content = new StringContent(JsonConvert.SerializeObject(movie))
            };

            return SendRequest<RadarrMovie>(request, cancellationToken);
        }

        public Task<IEnumerable<RadarrMovie>> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken)
        {
            var (dateTime, till) = dateRange;
            var request = new HttpRequestMessage
            (
                HttpMethod.Get, 
                new Uri($"{_config.Url}api/v3/calendar?apikey={_config.Key}&start={dateTime:yyyy-MM-dd}&end={till:yyyy-MM-dd}")
            );

            return SendRequest<IEnumerable<RadarrMovie>>(request, cancellationToken);
        }

        public Task<IEnumerable<RadarrQueue>> GetQueueAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri($"{_config.Url}api/v3/queue?apikey={_config.Key}"));

            return SendRequest<IEnumerable<RadarrQueue>>(request, cancellationToken);
        }

        public Task<HistoryResult<RadarrHistoryRecord>> GetMovieHistoryAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            (
                HttpMethod.Get,
                new Uri($"{_config.Url}api/v3/history?page=1&pageSize=15&sortKey=date&sortDir=asc&movieId={id}&apikey={_config.Key}")
            );

            return SendRequest<HistoryResult<RadarrHistoryRecord>>(request, cancellationToken);
        }
    }
}
