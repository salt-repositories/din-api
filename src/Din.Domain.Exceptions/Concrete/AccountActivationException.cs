using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class AccountActivationException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public AccountActivationException(string message) : base(message)
        {
        }
    }
}
