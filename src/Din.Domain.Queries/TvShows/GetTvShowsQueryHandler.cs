using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQueryHandler : ContentQueryHandler<SonarrTvShow>,
        IRequestHandler<GetTvShowsQuery, QueryResult<SonarrTvShow>>
    {
        public GetTvShowsQueryHandler(IPlexHelper plexHelper, IPosterHelper posterHelper,
            IContentStore<SonarrTvShow> store) : base(plexHelper, posterHelper, store)
        {
        }

        public async Task<QueryResult<SonarrTvShow>> Handle(GetTvShowsQuery request,
            CancellationToken cancellationToken)
        {
            var (tvShows, count) = Store.GetAll(request.QueryParameters, request.Filters);

            await RetrieveOptionalData(tvShows, request.Plex, request.Poster, cancellationToken);
            
            return new QueryResult<SonarrTvShow>(tvShows, count);
        }
    }
}