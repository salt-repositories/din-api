using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQueryHandler : IRequestHandler<GetTvShowsQuery, IEnumerable<SonarrTvShow>>
    {
        private readonly IContentStore<SonarrTvShow> _store;

        public GetTvShowsQueryHandler(IContentStore<SonarrTvShow> store)
        {
            _store = store;
        }

        public Task<IEnumerable<SonarrTvShow>> Handle(GetTvShowsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult((IEnumerable<SonarrTvShow>) _store.GetAll());
        }
    }
}
