using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQueryHandler : IRequestHandler<GetTvShowsQuery, QueryResult<SonarrTvShow>>
    {
        private readonly IContentStore<SonarrTvShow> _store;
        private readonly IPlexHelper _helper;

        public GetTvShowsQueryHandler(IContentStore<SonarrTvShow> store, IPlexHelper helper)
        {
            _store = store;
            _helper = helper;
        }

        public async Task<QueryResult<SonarrTvShow>> Handle(GetTvShowsQuery request, CancellationToken cancellationToken)
        {
            var (tvShows, count) = _store.GetAll(request.QueryParameters, request.Filters);

            if (request.Plex)
            {
                await _helper.CheckIsOnPlex(tvShows, cancellationToken);
            }

            return new QueryResult<SonarrTvShow>(tvShows, count);
        }
    }
}
