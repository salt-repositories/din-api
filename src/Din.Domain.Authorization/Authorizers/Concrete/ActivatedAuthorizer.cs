using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Requests;
using Din.Domain.Context;
using Din.Domain.Exceptions.Concrete;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Din.Domain.Authorization.Authorizers.Concrete
{
    public class ActivatedAuthorizer<TRequest> : IRequestAuthorizer<TRequest> where TRequest : IActivatedRequest
    {
        private readonly IRequestContext _context;
        private readonly IAccountRepository _repository;

        public ActivatedAuthorizer(IRequestContext context, IAccountRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Authorize(TRequest request)
        {
            var account = await _repository.GetAccountById(_context.GetIdentity(), CancellationToken.None);

            if (!account.Active)
            {
                throw new AuthorizationException("Requester is not active");
            }
        }
    }
}
