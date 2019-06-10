using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsByTitleQueryHandler : IRequestHandler<GetTvShowsByTitleQuery, IEnumerable<SonarrTvShow>>
    {
        private readonly IContentStore<SonarrTvShow> _store;

        public GetTvShowsByTitleQueryHandler(IContentStore<SonarrTvShow> store)
        {
            _store = store;
        }

        public Task<IEnumerable<SonarrTvShow>> Handle(GetTvShowsByTitleQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult((IEnumerable<SonarrTvShow>) _store.GetMultipleByTitle(request.Title));  
        }
    }
}
