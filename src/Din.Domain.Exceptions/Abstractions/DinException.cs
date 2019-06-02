using System;
using System.Net;

namespace Din.Domain.Exceptions.Abstractions
{
    public abstract class DinException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; }

        protected DinException(string message) : base(message)
        {
        }
    }
}
