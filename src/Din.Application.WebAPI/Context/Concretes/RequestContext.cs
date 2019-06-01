using System;
using System.IO;
using System.Linq;
using Din.Application.WebAPI.Context.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Din.Application.WebAPI.Context.Concretes
{
    public class RequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetIdentity()
        {
            var identityClaim = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "Identity");

            if (identityClaim == null)
            {
                throw new NullReferenceException("request identity is null");
            }

            if (!Guid.TryParse(identityClaim.Value, out var identity))
            {
                throw new InvalidDataException("Stored identity is not a valid guid");
            }

            return identity;
        }

        public string GetUserAgentAsString()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        }

        public string GetRequestIpAsString()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"].ToString();
        }
    }
}
