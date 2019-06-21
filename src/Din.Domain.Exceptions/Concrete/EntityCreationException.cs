using System.Net;
using Din.Domain.Exceptions.Abstractions;

namespace Din.Domain.Exceptions.Concrete
{
    public class EntityCreationException : DinException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public EntityCreationException(string message) : base(message)
        {
        }
    }
}
