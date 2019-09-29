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
            var tvShows = _store.GetAll(request.QueryParameters, request.Title);
            var count = _store.Count(request.Title);

            return Task.FromResult(new QueryResult<SonarrTvShow>(tvShows, count));
        }
    }
}
