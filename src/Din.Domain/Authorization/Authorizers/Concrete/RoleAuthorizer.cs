using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Requests;
using Din.Domain.Context;
using Din.Domain.Exceptions;

namespace Din.Domain.Authorization.Authorizers.Concrete
{
    public class RoleAuthorizer<TRequest> : IRequestAuthorizer<TRequest>  where TRequest : IAuthorizedAccountRequest
    {
        private readonly IRequestContext _context;

        public RoleAuthorizer(IRequestContext context)
        {
            _context = context;
        }

        public Task Authorize(TRequest request)
        { 
            var role = _context.GetAccountRole();

            if (role != request.Role)
            {
                throw new AuthorizationException("Account is not authorized to perform this operation");
            }

           return Task.CompletedTask;
        }
    }
}
