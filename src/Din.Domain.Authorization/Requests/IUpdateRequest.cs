using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Din.Domain.Authorization.Requests
{
    public interface IUpdateRequest<T> : IBaseRequest where T : class
    {
        JsonPatchDocument<T> Update { get; }
    }
}
