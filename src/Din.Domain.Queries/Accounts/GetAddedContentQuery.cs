using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAddedContentQuery : RequestWithQueryParameters, ITransactionRequest, IAuthorizedIdentityRequest, IActivatedRequest, IRequest<QueryResult<AddedContent>>
    {
        public Guid Identity { get; }
        public AddedContentFilters Filters { get; }

        public GetAddedContentQuery(QueryParameters queryParameters, Guid identity, AddedContentFilters filters) : base(queryParameters)
        {
            Identity = identity;
            Filters = filters;
        }
    }
}
