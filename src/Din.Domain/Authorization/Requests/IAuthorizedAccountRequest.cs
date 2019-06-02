using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Authorization.Requests
{
    public interface IAuthorizedAccountRequest : IBaseRequest
    {
        AccountRole Role { get; set; }
    }
}
