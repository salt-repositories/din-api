using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Context;
using Din.Domain.Authorization.Requests;
using Din.Domain.Exceptions.Concrete;

namespace Din.Domain.Authorization.Authorizers.Concrete
{
    public class RoleAuthorizer<TRequest> : IRequestAuthorizer<TRequest>  where TRequest : IAuthorizedRoleRequest
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
                throw new AuthorizationException("Requester is not authorized to perform this operation");
            }

           return Task.CompletedTask;
        }
    }
}
