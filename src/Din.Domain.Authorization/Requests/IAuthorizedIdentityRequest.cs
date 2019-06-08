using System;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Authorization.Requests
{
    public interface IAuthorizedIdentityRequest : IBaseRequest
    {
        Guid Identity { get; }
    }
}
