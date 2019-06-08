using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, RadarrMovie>
    {
        private readonly IMediaStore _store;
        private readonly IRadarrClient _client;

        public GetMovieByIdQueryHandler(IMediaStore store, IRadarrClient client)
        {
            _store = store;
            _client = client;
        }

        public async Task<RadarrMovie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = _store.GetMovieById(request.Id);

            if (movie != null)
            {
                return movie;
            }

            return await _client.GetMovieByIdAsync(request.Id, cancellationToken);
        }
    }
}