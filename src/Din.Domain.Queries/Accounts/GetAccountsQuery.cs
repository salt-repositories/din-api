using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAccountsQuery : RequestWithQueryParameters<Account>, IAuthorizedRoleRequest, IRequest<QueryResult<Account>>
    {
        public AccountRole Role { get; }

        public GetAccountsQuery(QueryParameters<Account> queryParameters) : base(queryParameters)
        {
            Role = AccountRole.Admin;
        }
    }
}
