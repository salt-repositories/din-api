using System;
using System.Net;

namespace Din.Domain.Exceptions.Abstractions
{
    public abstract class DinException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; }
        public object Details { get; }

        protected DinException(string message, object details) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Details = details;
        }
    }
}
