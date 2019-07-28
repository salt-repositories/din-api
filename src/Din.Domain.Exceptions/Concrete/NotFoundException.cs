using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class NotFoundException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundException(string message, object details) : base(message, details)
        {
        }
    }
}
