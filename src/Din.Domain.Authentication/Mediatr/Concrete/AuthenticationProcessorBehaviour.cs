using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Authorization.Context;
using Din.Domain.Authorization.Mediatr.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using MediatR.Pipeline;
using UAParser;

namespace Din.Domain.Authentication.Mediatr.Concrete
{
    public class AuthenticationProcessorBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IAuthenticationRequest
    {
        private readonly IRequestContext _context;

        public AuthenticationProcessorBehaviour(IRequestContext context)
        {
            _context = context;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (request.GetType() == typeof())
            {
                var clientInfo = Parser.GetDefault().Parse(_context.GetUserAgentAsString());
                var loginAttempt = new LoginAttempt
                {
                    Username = (request as AuthenticationDto).Username,

                };
            }
        }
    }
}
