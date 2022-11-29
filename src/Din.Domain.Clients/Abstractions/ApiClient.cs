﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public abstract class ApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        protected ApiClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected static HttpContent JsonContent<T>(T content) =>
            new StringContent(JsonConvert.SerializeObject(content), Encoding.Default, "application/json");

        protected async Task SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var client = _clientFactory.CreateClient())
            {
                SetClientProperties(client);

                var response = await Request(client, request, cancellationToken);

                await CheckResponse(request, response);
            }
        }

        protected async Task<T> SendRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var client = _clientFactory.CreateClient())
            {
                SetClientProperties(client);

                var response = await Request(client, request, cancellationToken);

                await CheckResponse(request, response);

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private void SetClientProperties(HttpClient client)
        {
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("User-Agent", "DinApi");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        private async Task<HttpResponseMessage> Request(HttpClient client, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                return await client.SendAsync(request, cancellationToken);
            }
            catch (Exception exception)
            {
                if (exception is TimeoutException || exception is TaskCanceledException)
                {
                    throw new HttpClientException($"[{GetType().Name}]: Timeout", null);
                }

                throw;
            }
        }

        private async Task CheckResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                var path = $"{request.RequestUri.Scheme}://{request.RequestUri.Host}{request.RequestUri.AbsolutePath}";

                throw new HttpClientException(
                    $"[{GetType().Name}]: {request.Method} {path} [{response.StatusCode}]",
                    await response.Content.ReadAsStringAsync()
                );
            }
        }
    }
}