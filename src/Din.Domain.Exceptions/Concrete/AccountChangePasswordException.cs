using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class AccountChangePasswordException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public AccountChangePasswordException(string message) : base(message, null)
        {
        }
    }
}