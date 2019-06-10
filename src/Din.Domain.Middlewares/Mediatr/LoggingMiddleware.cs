using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Logging.Handlers.Interfaces;
using MediatR;
using MediatR.Pipeline;

namespace Din.Domain.Middlewares.Mediatr
{
    public class LoggingMiddleware<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IBaseRequest
    {
        private readonly ILoggingHandler<TRequest, TResponse> _loggingHandler;

        public LoggingMiddleware(ILoggingHandler<TRequest, TResponse> loggingHandler)
        {
            _loggingHandler = loggingHandler;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return _loggingHandler.Log(request, response, cancellationToken);
        }
    }
}
