using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class AuthorizationException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

        public AuthorizationException(string message = "Requester is not authorized to perform this operation") : base(message, null)
        {
        }
    }
}
