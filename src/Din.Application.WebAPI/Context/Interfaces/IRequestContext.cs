using System;

namespace Din.Application.WebAPI.Context.Interfaces
{
    public interface IRequestContext
    {
        Guid GetIdentity();
        string GetUserAgentAsString();
        string GetRequestIpAsString();
    }
}
