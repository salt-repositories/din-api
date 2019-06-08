using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesByTitleQueryHandler : IRequestHandler<GetMoviesByTitleQuery, IEnumerable<RadarrMovie>>
    {
        private readonly IMediaStore _store;
        private readonly IRadarrClient _client;

        public GetMoviesByTitleQueryHandler(IMediaStore store, IRadarrClient client)
        {
            _store = store;
            _client = client;
        }

        public async Task<IEnumerable<RadarrMovie>> Handle(GetMoviesByTitleQuery request, CancellationToken cancellationToken)
        {
            var movies = _store.GetMoviesByTitle(request.Title);

            if (movies != null)
            {
                return movies;
            }

            _store.SetMovies(await _client.GetMoviesAsync(cancellationToken));

            return _store.GetMoviesByTitle(request.Title);
        }
    }
}
