using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, QueryResult<Account>>
    {
        private readonly IAccountRepository _repository;

        public GetAccountsQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<Account>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAccounts(request.QueryParameters, cancellationToken);
            var count = await _repository.Count<Account>(cancellationToken);

            return new QueryResult<Account>(accounts, count);
        }
    }
}