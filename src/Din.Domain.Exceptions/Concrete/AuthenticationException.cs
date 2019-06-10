using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class AuthenticationException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public AuthenticationException(string message) : base(message)
        {
        }
    }
}