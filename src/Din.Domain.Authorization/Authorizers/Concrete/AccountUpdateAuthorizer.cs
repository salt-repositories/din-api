using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Requests;
using Din.Domain.Context;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;

namespace Din.Domain.Authorization.Authorizers.Concrete
{
    public class AccountUpdateAuthorizer<TRequest> : IRequestAuthorizer<TRequest> where TRequest : IUpdateRequest<Account>
    {
        private readonly IRequestContext _context;

        public AccountUpdateAuthorizer(IRequestContext context)
        {
            _context = context;
        }

        public Task Authorize(TRequest request)
        {
            if (request.Update.Operations.FirstOrDefault(o => o.path.Equals("role")) != null && _context.GetAccountRole() != AccountRole.Admin)
            {
                throw new AuthorizationException();
            }

            return Task.CompletedTask;
        }
    }
}
