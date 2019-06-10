using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using MediatR;
using TMDbLib.Client;
using TMDbLib.Objects.TvShows;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowFromTmdbQueryHandler : IRequestHandler<GetTvShowFromTmdbQuery, IEnumerable<TvShow>>
    {
        private readonly TMDbClient _client;

        public GetTvShowFromTmdbQueryHandler(ITmdbClientConfig config)
        {
            _client = new TMDbClient(config.Key);
        }

        public async Task<IEnumerable<TvShow>> Handle(GetTvShowFromTmdbQuery request, CancellationToken cancellationToken)
        {
            ICollection<TvShow> tvShows = new List<TvShow>();
            var results = (await _client.SearchTvShowAsync(request.Query, 0, cancellationToken)).Results;

            foreach (var result in results)
            {
                tvShows.Add(await _client.GetTvShowAsync(result.Id, TvShowMethods.ExternalIds, null, cancellationToken));
            }

            return tvShows;
        }
    }
}
