using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Logging.Handlers.Interfaces;
using Din.Domain.Logging.Loggers.Interfaces;
using MediatR;

namespace Din.Domain.Logging.Handlers.Concrete
{
    public class LoggingHandler<TRequest, TResponse> : ILoggingHandler<TRequest, TResponse> where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IRequestLogger<TRequest, TResponse>> _loggers;

        public LoggingHandler(IEnumerable<IRequestLogger<TRequest, TResponse>> loggers)
        {
            _loggers = loggers;
        }

        public async Task Log(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            foreach (var logger in _loggers)
            {
                await logger.Log(request, response, cancellationToken);
            }
        }
    }
}
