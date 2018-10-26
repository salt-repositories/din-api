using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMovieClientConfig _config;

        public MovieClient(IHttpClientFactory httpClientFactory, IMovieClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IEnumerable<McMovie>> GetCurrentMoviesAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<McMovie>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "movie", $"?apikey={_config.Key}")));
        }

        public async Task<McMovie> GetMovieByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<McMovie>(
                await client.GetStringAsync(BuildUrl(_config.Url, $"movie/{id}", $"?apikey={_config.Key}")));
        }

        public async Task<(bool status, int systemId)> AddMovieAsync(McRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(_config.Url, "movie", $"?apikey={_config.Key}"),
                new StringContent(JsonConvert.SerializeObject(movie)));

            return (response.StatusCode.Equals(HttpStatusCode.Created), JsonConvert.DeserializeObject<McMovie>(await response.Content.ReadAsStringAsync()).SystemId);
        }

        public async Task<IEnumerable<McCalendar>> GetCalendarAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<McCalendar>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "calendar", $"?apikey={_config.Key}",
                    GetCalendarTimeSpan())));
        }

        public async Task<IEnumerable<McQueueItem>> GetQueue()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<McQueueItem>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "queue", $"?apikey={_config.Key}")));
        }
    }
}