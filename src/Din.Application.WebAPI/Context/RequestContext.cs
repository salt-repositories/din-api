using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Din.Domain.Authorization.Context;
using Din.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Din.Application.WebAPI.Context
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
            var identityClaim = GetClaimByType("Identity");

            if (!Guid.TryParse(identityClaim.Value, out var identity))
            {
                throw new InvalidDataException("Stored identity is not a valid guid");
            }

            return identity;
        }

        public AccountRole GetAccountRole()
        {
            var roleClaim = GetClaimByType("Role");

            if (!Enum.TryParse<AccountRole>(roleClaim.Value, out var role))
            {
                throw new InvalidDataException("Stored role is not a valid account role");
            }

            return role;
        }

        public string GetUserAgentAsString()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        }

        public string GetRequestIpAsString()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"].ToString();
        }

        private Claim GetClaimByType(string type)
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == type);

            return claim ?? throw new NullReferenceException("request identity is null");
        }
    }
}
