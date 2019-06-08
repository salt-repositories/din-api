using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.Logging.Handlers.Interfaces
{
    public interface ILoggingHandler<in TRequest, in TResponse>
    {
        Task Log(TRequest request, TResponse response, CancellationToken cancellationToken);
    }
}
