using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Handlers.Interfaces;
using MediatR;

namespace Din.Domain.Authorization.Handlers.Concrete
{
    public class AuthorizationHandler<TRequest> : IAuthorizationHandler<TRequest> where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IRequestAuthorizer<TRequest>> _requestAuthorizers;

        public AuthorizationHandler(IEnumerable<IRequestAuthorizer<TRequest>> requestAuthorizers)
        {
            _requestAuthorizers = requestAuthorizers;
        }

        public async Task Authorize(TRequest input)
        {
            foreach (var authorizer in _requestAuthorizers)
            {
                await authorizer.Authorize(input).ConfigureAwait(false);
            }
        }
    }

}