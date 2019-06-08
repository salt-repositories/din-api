using System.Net;
using System.Net.Http;
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

        protected async Task SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                await CheckResponse(request, response);
            }
        }

        protected async Task<T> SendRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                await CheckResponse(request, response);

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private async Task CheckResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var path = $"{request.RequestUri.Scheme}://{request.RequestUri.Host}{request.RequestUri.AbsolutePath}";
                throw new HttpClientException(
                    $"[{GetType().Name}]: {request.Method} {path} \n Response: {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}