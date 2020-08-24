using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Logging.Loggers.Interfaces;
using Din.Domain.Models.Dtos;
using MediatR.Pipeline;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandExceptionHandler : IRequestExceptionHandler<GenerateTokenCommand, TokenDto, AuthenticationException>
    {
        private readonly IEnumerable<IRequestLogger<GenerateTokenCommand, TokenDto>> _loggers;

        public GenerateTokenCommandExceptionHandler(IEnumerable<IRequestLogger<GenerateTokenCommand, TokenDto>> loggingHandlers)
        {
            _loggers = loggingHandlers;
        }

        public async Task Handle(GenerateTokenCommand request, AuthenticationException exception, RequestExceptionHandlerState<TokenDto> state,
            CancellationToken cancellationToken)
        {
            foreach (var logger in _loggers)
            {
                await logger.Log(request, state.Response, cancellationToken);
            }
        }
    }
}
