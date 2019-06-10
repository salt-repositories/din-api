using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAccountQuery : IAuthorizedIdentityRequest, IRequest<Account>
    {
        public Guid Identity { get; }
        public GetAccountQuery(Guid id)
        {
            Identity = id;
        }
    }
}
