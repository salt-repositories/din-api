using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAddedContentQueryHandler : IRequestHandler<GetAddedContentQuery, QueryResult<AddedContent>>
    {
        private readonly IAddedContentRepository _repository;

        public GetAddedContentQueryHandler(IAddedContentRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<AddedContent>> Handle(GetAddedContentQuery request, CancellationToken cancellationToken)
        {
            var addedContent = await _repository.GetAddedContentByAccountId(request.QueryParameters, request.Identity, cancellationToken);
            var count = await _repository.Count<AddedContent>(cancellationToken);

            return new QueryResult<AddedContent>(addedContent, count);
        }
    }
}
