using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Newtonsoft.Json;

namespace Din.Domain.Clients
{
    public abstract class ApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        protected ApiClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected async Task SendRequest(HttpRequestMessage request)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                CheckResponse(request, response);
            }
        }

        protected async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.SendAsync(request);

                CheckResponse(request, response);

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private void CheckResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpClientException($"{GetType()} ${request.Method} ${request.RequestUri}: ${response.StatusCode}");
            }
        }
    }
}
