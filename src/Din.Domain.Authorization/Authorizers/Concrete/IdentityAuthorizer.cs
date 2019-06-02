using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Requests;
using Din.Domain.Context;
using Din.Domain.Exceptions.Concrete;

namespace Din.Domain.Authorization.Authorizers.Concrete
{
    public class IdentityAuthorizer<TRequest> : IRequestAuthorizer<TRequest> where TRequest : IAuthorizedIdentityRequest
    {
        private readonly IRequestContext _context;

        public IdentityAuthorizer(IRequestContext context)
        {
            _context = context;
        }

        public Task Authorize(TRequest request)
        {
            var identity = _context.GetIdentity();

            if (identity != request.Identity)
            {
                throw new AuthorizationException("Requester is not authorized to perform this operation");
            }

            return Task.CompletedTask;
        }
    }
}
