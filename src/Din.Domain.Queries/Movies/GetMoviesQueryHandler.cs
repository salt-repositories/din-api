using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, QueryResult<RadarrMovie>>
    {
        private readonly IContentStore<RadarrMovie> _store;

        public GetMoviesQueryHandler(IContentStore<RadarrMovie> store)
        {
            _store = store;
        }

        public Task<QueryResult<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var (movies, count) = _store.GetAll(request.QueryParameters, request.Filters);
            
            return Task.FromResult(new QueryResult<RadarrMovie>(movies, count));
        }
    }
}
