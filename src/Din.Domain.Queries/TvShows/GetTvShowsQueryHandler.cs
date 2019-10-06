using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQueryHandler : IRequestHandler<GetTvShowsQuery, QueryResult<SonarrTvShow>>
    {
        private readonly IContentStore<SonarrTvShow> _store;

        public GetTvShowsQueryHandler(IContentStore<SonarrTvShow> store)
        {
            _store = store;
        }

        public Task<QueryResult<SonarrTvShow>> Handle(GetTvShowsQuery request, CancellationToken cancellationToken)
        {
            var (tvShows, count) = _store.GetAll(request.QueryParameters, request.Filters);

            return Task.FromResult(new QueryResult<SonarrTvShow>(tvShows, count));
        }
    }
}
