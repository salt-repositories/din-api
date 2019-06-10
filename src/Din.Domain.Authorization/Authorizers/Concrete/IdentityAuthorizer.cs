using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Requests;
using Din.Domain.Context;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;

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
            var role = _context.GetAccountRole();

            if (identity != request.Identity && role != AccountRole.Admin)
            {
                throw new AuthorizationException();
            }

            return Task.CompletedTask;
        }
    }
}
