using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class MovieClient : IMovieClient
    {
        private readonly HttpClient _client;
        private readonly IMovieClientConfig _config;

        public MovieClient(HttpClient httpClient, IMovieClientConfig config)
        {
            httpClient.BaseAddress = new Uri(config.Url);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _config = config;
        }

        public async Task<T> GetCurrentMoviesAsync<T>(int pageSize, int page, string sortKey, string sortDirection)
        {
            return JsonConvert.DeserializeObject<T>(
                await _client.GetStringAsync($"movie?apikey={_config.Key}&pageSize={pageSize}&page={page}&sortKey={sortKey}&sortDir={sortDirection}"));
        }

        public async Task<McMovie> GetMovieByIdAsync(int id)
        {
            return JsonConvert.DeserializeObject<McMovie>(
                await _client.GetStringAsync($"movie/{id}?apikey={_config.Key}"));
        }

        public async Task<(bool status, int systemId)> AddMovieAsync(McRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;

            var response = await _client.PostAsync($"movie?apikey={_config.Key}",
                new StringContent(JsonConvert.SerializeObject(movie)));

            return (response.StatusCode.Equals(HttpStatusCode.Created),
                JsonConvert.DeserializeObject<McMovie>(await response.Content.ReadAsStringAsync()).SystemId);
        }

        public async Task<IEnumerable<T>> GetCalendarAsync<T>(DateTime start, DateTime end)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(await _client.GetStringAsync($"calendar?apikey={_config.Key}&start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}"));
        }

        public async Task<IEnumerable<T>> GetQueue<T>()
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(
                await _client.GetStringAsync($"queue?apikey={_config.Key}"));
        }
    }
}