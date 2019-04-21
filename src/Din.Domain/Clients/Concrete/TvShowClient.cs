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
    public class TvShowClient : ITvShowClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITvShowClientConfig _config;

        public TvShowClient(IHttpClientFactory clientFactory, ITvShowClientConfig config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<IEnumerable<TcTvShow>> GetCurrentTvShowsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}series?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET tv shows failed");
                }

                return JsonConvert.DeserializeObject<IEnumerable<TcTvShow>>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<TcTvShow> GetTvShowByIdAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_config.Url}series/{id}?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpClientException($"{GetType()} GET tv show by id failed");
                }

                return JsonConvert.DeserializeObject<TcTvShow>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<TcTvShow> AddTvShowAsync(TcRequest tvShow)
        {
            tvShow.RootFolderPath = _config.SaveLocation;

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_config.Url}series?apikey={_config.Key}"));
            request.Headers.Add("Accept", "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new HttpClientException($"{GetType()} POST tv show failed");
                }

                return JsonConvert.DeserializeObject<TcTvShow>(await response.Content.ReadAsStringAsync());
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
                    throw new HttpClientException($"{GetType()} GET calendar by date range failed");
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