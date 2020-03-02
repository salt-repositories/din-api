using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Extensions;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, QueryResult<RadarrMovie>>
    {
        private readonly IContentStore<RadarrMovie> _store;
        private readonly IPlexClient _client;
        private readonly IPlexConfig _config;

        public GetMoviesQueryHandler(IContentStore<RadarrMovie> store, IPlexClient client, IPlexConfig config)
        {
            _store = store;
            _client = client;
            _config = config;
        }

        public Task<QueryResult<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var (movies, count) = _store.GetAll(request.QueryParameters, request.Filters);

            if (request.Plex)
            {
                Parallel.ForEach(movies, async (movie) =>
                {
                    var response = await _client.SearchByTitle(movie.Title.ToLower(), cancellationToken);

                    if 
                    (
                        response.MediaContainer?.Metadata?.Length > 0 &&
                        response.MediaContainer.Metadata[0].Type.Equals("movie") &&
                        response.MediaContainer.Metadata[0].Title.CalculateSimilarity(movie.Title) > 0.6
                    )
                    {
                        movie.PlexUrl =
                            $"https://app.plex.tv/desktop#!/server/{_config.ServerGuid}/details?key={response.MediaContainer.Metadata[0].Key}";
                    }
                });
            }

            return Task.FromResult(new QueryResult<RadarrMovie>(movies, count));
        }
    }
}
