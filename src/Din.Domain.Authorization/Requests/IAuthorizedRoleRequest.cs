using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Authorization.Requests
{
    public interface IAuthorizedRoleRequest : IBaseRequest
    {
        AccountRole AuthorizedRole { get; }
    }
}
