using System;
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

        protected async Task<T> SendRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var client = _clientFactory.CreateClient();

            SetClientProperties(client);
            var response = await Request(client, request, cancellationToken);
            var responseBody = await CheckResponse(request, response);

            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        private static void SetClientProperties(HttpClient client)
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
                if (exception is TimeoutException or TaskCanceledException)
                {
                    throw new HttpClientException($"[{GetType().Name}]: Timeout", null);
                }

                throw;
            }
        }

        private async Task<string> CheckResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode is >= 200 and < 300)
            {
                return responseBody;
            }

            var path = $"{request.RequestUri?.Scheme}://{request.RequestUri?.Host}{request.RequestUri?.AbsolutePath}";

            throw new HttpClientException(
                $"[{GetType().Name}]: {request.Method} {path} [{response.StatusCode}]",
                responseBody
            );
        }
    }
}