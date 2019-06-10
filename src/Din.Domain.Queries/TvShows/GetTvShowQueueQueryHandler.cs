using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowQueueQueryHandler : IRequestHandler<GetTvShowQueueQuery, IEnumerable<SonarrQueue>>
    {
        private readonly ISonarrClient _client;

        public GetTvShowQueueQueryHandler(ISonarrClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<SonarrQueue>> Handle(GetTvShowQueueQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetQueueAsync(cancellationToken);
        }
    }
}
