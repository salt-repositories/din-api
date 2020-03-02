using System;
using MediatR;

namespace Din.Domain.Authorization.Requests
{
    public interface IAuthorizedIdentityRequest : IBaseRequest
    {
        Guid Identity { get; }
    }
}
