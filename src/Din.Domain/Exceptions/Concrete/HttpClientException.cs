using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class HttpClientException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public HttpClientException(string message) : base(message)
        {
        }
    }
}
