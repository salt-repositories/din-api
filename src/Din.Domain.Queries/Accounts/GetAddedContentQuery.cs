using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAddedContentQuery : RequestWithQueryParameters<AddedContent>, IAuthorizedIdentityRequest, IActivatedRequest, IRequest<QueryResult<AddedContent>>
    {
        public Guid Identity { get; }

        public GetAddedContentQuery(QueryParameters<AddedContent> queryParameters, Guid identity) : base(queryParameters)
        {
            Identity = identity;
        }
    }
}
