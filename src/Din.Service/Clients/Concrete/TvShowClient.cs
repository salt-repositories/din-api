using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class TvShowClient : BaseClient, ITvShowClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITvShowClientConfig _config;

        public TvShowClient(IHttpClientFactory httpClientFactory, ITvShowClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IEnumerable<TcTvShow>> GetCurrentTvShowsAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<TcTvShow>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "series", $"?apikey={_config.Key}")));
        }

        public async Task<TcTvShow> GetTvShowByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<TcTvShow>(
                await client.GetStringAsync(BuildUrl(_config.Url, $"series/{id}", $"?apikey={_config.Key}")));
        }

        public async Task<(bool status, int systemId)> AddTvShowAsync(TcRequest tvShow)
        {
            tvShow.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(_config.Url, "series", $"?apikey={_config.Key}"),
                new StringContent(JsonConvert.SerializeObject(tvShow)));

            return (response.StatusCode.Equals(HttpStatusCode.Created),
                JsonConvert.DeserializeObject<TcTvShow>(await response.Content.ReadAsStringAsync()).SystemId);
        }

        public async Task<IEnumerable<TcCalendar>> GetCalendarAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<TcCalendar>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "calendar", $"?apikey={_config.Key}",
                    GetCalendarTimeSpan())));
        }

        public async Task<IEnumerable<TcQueueItem>> GetQueue()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<TcQueueItem>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "queue", $"?apikey={_config.Key}")));
        }
    }
}