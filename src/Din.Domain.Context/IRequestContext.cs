using System;
using Din.Domain.Models.Entities;

namespace Din.Domain.Context
{
    public interface IRequestContext
    {
        Guid GetIdentity();
        AccountRole GetAccountRole();
        string GetUserAgentAsString();
        string GetRequestIpAsString();
    }
}
