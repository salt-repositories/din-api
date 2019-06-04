using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IAccountRepository _repository;

        public GetAccountQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAccount(request.Identity, cancellationToken);
        }
    }
}
