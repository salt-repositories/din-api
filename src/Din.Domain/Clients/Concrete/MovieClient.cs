using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Din.Domain.Exceptions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class MovieClient : IMovieClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMovieClientConfig _config;

        public MovieClient(IHttpClientFactory clientFactory, IMovieClientConfig config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<T> GetCurrentMoviesAsync<T>(int pageSize, int page, string sortKey, string sortDirection)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}movie?apikey={_config.Key}&pageSize={pageSize}&page={page}&sortKey={sortKey}&sortDir={sortDirection}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET movies failed");
                }

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<McMovie> GetMovieByIdAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}movie/{id}?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET movies by id failed");
                }

                return JsonConvert.DeserializeObject<McMovie>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<McMovie> AddMovieAsync(McRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_config.Url}movie?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new HttpClientException($"{GetType()} POST movie failed");
                }

                return JsonConvert.DeserializeObject<McMovie>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<T>> GetCalendarAsync<T>(DateTime start, DateTime end)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}calendar?apikey={_config.Key}&start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET calender by date range failed");
                }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<T>> GetQueue<T>()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}queue?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET queue failed");
                }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}