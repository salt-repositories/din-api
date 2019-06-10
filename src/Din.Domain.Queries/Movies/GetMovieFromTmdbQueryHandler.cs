using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using MediatR;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieFromTmdbQueryHandler : IRequestHandler<GetMovieFromTmdbQuery, IEnumerable<SearchMovie>>
    {
        private readonly TMDbClient _client;

        public GetMovieFromTmdbQueryHandler(ITmdbClientConfig config)
        {
            _client = new TMDbClient(config.Key);
        }

        public async Task<IEnumerable<SearchMovie>> Handle(GetMovieFromTmdbQuery request,
            CancellationToken cancellationToken)
        {
            return (await _client.SearchMovieAsync(request.Query, 0, false, 0, cancellationToken)).Results;
        }
    }
}