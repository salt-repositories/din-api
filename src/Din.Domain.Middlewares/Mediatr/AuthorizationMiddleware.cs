using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Authorization.Handlers.Interfaces;
using MediatR;
using MediatR.Pipeline;

namespace Din.Domain.Middlewares.Mediatr
{
    public class AuthorizationMiddleware<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IBaseRequest
    {
        private readonly IAuthorizationHandler<TRequest> _authorizationHandler;

        public AuthorizationMiddleware(IAuthorizationHandler<TRequest> authorizationHandler)
        {
            _authorizationHandler = authorizationHandler;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            return _authorizationHandler.Authorize(request);
        }
    }
}
