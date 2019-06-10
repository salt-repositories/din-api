using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Din.Domain.Logging.Loggers.Interfaces
{
    public interface IRequestLogger<in TRequest, in TResponse> where TRequest : IBaseRequest
    {
        Task Log(TRequest request, TResponse response, CancellationToken cancellationToken);
    }
}
