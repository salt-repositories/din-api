using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using MediatR;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowFromTmdbQueryHandler : IRequestHandler<GetTvShowFromTmdbQuery, IEnumerable<SearchTv>>
    {
        private readonly TMDbClient _client;

        public GetTvShowFromTmdbQueryHandler(ITmdbClientConfig config)
        {
            _client = new TMDbClient(config.Key);
        }

        public async Task<IEnumerable<SearchTv>> Handle(GetTvShowFromTmdbQuery request, CancellationToken cancellationToken)
        {
            return (await _client.SearchTvShowAsync(request.Query, 0, cancellationToken)).Results;
        }
    }
}
