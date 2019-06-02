using System;
using System.Net;

namespace Din.Domain.Exceptions
{
    public class AuthorizationException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public AuthorizationException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }
    }
}
