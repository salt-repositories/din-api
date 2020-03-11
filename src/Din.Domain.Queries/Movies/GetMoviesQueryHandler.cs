using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : ContentRetrievalQueryHandler<RadarrMovie>,
        IRequestHandler<GetMoviesQuery, QueryResult<RadarrMovie>>
    {
        public GetMoviesQueryHandler(IPlexHelper plexHelper, IPosterHelper posterHelper,
            IContentStore<RadarrMovie> store) : base(plexHelper, posterHelper, store)
        {
        }

        public async Task<QueryResult<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var (movies, count) = Store.GetAll(request.QueryParameters, request.Filters);

            await RetrieveOptionalData(movies, request.Plex, request.Poster, cancellationToken);

            return new QueryResult<RadarrMovie>(movies, count);
        }
    }
}