using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQueryHandler : ContentRetrievalQueryHandler<SonarrTvShow>,
        IRequestHandler<GetTvShowByIdQuery, SonarrTvShow>
    {
        public GetTvShowByIdQueryHandler(IPlexHelper plexHelper, IPosterHelper posterHelper,
            IContentStore<SonarrTvShow> store) : base(plexHelper, posterHelper, store)
        {
        }

        public async Task<SonarrTvShow> Handle(GetTvShowByIdQuery request, CancellationToken cancellationToken)
        {
            var item = Store.GetOneById(request.Id);

            await RetrieveOptionalData(new[] {item}, request.Plex, request.Poster, cancellationToken);
            
            return item;
        }
    }
}