using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<RadarrMovie>>
    {
        private readonly IMediaStore _store;
        private readonly IRadarrClient _client;

        public GetMoviesQueryHandler(IMediaStore store, IRadarrClient client)
        {
            _store = store;
            _client = client;
        }

        public async Task<IEnumerable<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = _store.GetMovies();

            if (movies != null)
            {
                return movies;
            }

            var newMovies = (await _client.GetMoviesAsync(cancellationToken)).ToList();
            _store.SetMovies(newMovies);

            return newMovies;
        }
    }
}
