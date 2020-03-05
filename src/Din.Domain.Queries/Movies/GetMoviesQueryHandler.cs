using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, QueryResult<RadarrMovie>>
    {
        private readonly IContentStore<RadarrMovie> _store;
        private readonly IPlexHelper _helper;

        public GetMoviesQueryHandler(IContentStore<RadarrMovie> store, IPlexHelper helper)
        {
            _store = store;
            _helper = helper;
        }

        public async Task<QueryResult<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var (movies, count) = _store.GetAll(request.QueryParameters, request.Filters);

            if (request.Plex)
            {
                await _helper.CheckIsOnPlex(movies, cancellationToken);
            }

            return new QueryResult<RadarrMovie>(movies, count);
        }
    }
}
