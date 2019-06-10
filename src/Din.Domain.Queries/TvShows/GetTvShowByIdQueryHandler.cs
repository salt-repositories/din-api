using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQueryHandler : IRequestHandler<GetTvShowByIdQuery, SonarrTvShow>
    {
        private readonly IContentStore<SonarrTvShow> _store;

        public GetTvShowByIdQueryHandler(IContentStore<SonarrTvShow> store)
        {
            _store = store;
        }

        public Task<SonarrTvShow> Handle(GetTvShowByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_store.GetOneById(request.Id));
        }
    }
}
