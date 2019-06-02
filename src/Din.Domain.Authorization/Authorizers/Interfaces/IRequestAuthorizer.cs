using System.Threading.Tasks;
using MediatR;

namespace Din.Domain.Authorization.Authorizers.Interfaces
{
    public interface IRequestAuthorizer<in TRequest> where TRequest : IBaseRequest
    {
        Task Authorize(TRequest request);
    }
}
