using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAccountsQuery : RequestWithQueryParameters<Account>, IRequest<QueryResult<Account>>
    {
        public GetAccountsQuery(QueryParameters<Account> queryParameters) : base(queryParameters)
        {
        }
    }
}
