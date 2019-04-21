using System;

namespace Din.Domain.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpClientException(string message) : base(message)
        {
        }
    }
}
