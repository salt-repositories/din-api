using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Concrete
{
    public class TvShowClient : ITvShowClient
    {
        private readonly HttpClient _client;
        private readonly ITvShowClientConfig _config;

        public TvShowClient(IHttpClientFactory clientFactory, ITvShowClientConfig config)
        {
            var httpClient = clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(config.Url);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<TcTvShow>> GetCurrentTvShowsAsync()
        {
            return JsonConvert.DeserializeObject<IEnumerable<TcTvShow>>(
                await _client.GetStringAsync($"series?apikey={_config.Key}"));
        }

        public async Task<TcTvShow> GetTvShowByIdAsync(int id)
        {
            return JsonConvert.DeserializeObject<TcTvShow>(
                await _client.GetStringAsync($"series/{id}?apikey={_config.Key}"));
        }

        public async Task<(bool status, int systemId)> AddTvShowAsync(TcRequest tvShow)
        {
            tvShow.RootFolderPath = _config.SaveLocation;

            var response = await _client.PostAsync($"series?apikey={_config.Key}",
                new StringContent(JsonConvert.SerializeObject(tvShow)));

            return (response.StatusCode.Equals(HttpStatusCode.Created),
                JsonConvert.DeserializeObject<TcTvShow>(await response.Content.ReadAsStringAsync()).SystemId);
        }

        public async Task<IEnumerable<T>> GetCalendarAsync<T>(DateTime start, DateTime end)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(
                await _client.GetStringAsync($"calendar?apikey={_config.Key}&start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}"));
        }

        public async Task<IEnumerable<T>> GetQueue<T>()
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(
                await _client.GetStringAsync($"queue?apikey={_config.Key}"));
        }
    }
}