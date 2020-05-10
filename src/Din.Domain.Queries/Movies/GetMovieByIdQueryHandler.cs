using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQueryHandler : ContentQueryHandler<RadarrMovie>,
        IRequestHandler<GetMovieByIdQuery, RadarrMovie>
    {
        public GetMovieByIdQueryHandler(IPlexHelper plexHelper, IPosterHelper posterHelper,
            IContentStore<RadarrMovie> store) : base(plexHelper, posterHelper, store)
        {
        }

        public async Task<RadarrMovie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var item = Store.GetOneById(request.Id);

            await RetrieveOptionalData(new[] {item}, request.ContentQueryParameters, cancellationToken);

            return item;
        }
    }
}