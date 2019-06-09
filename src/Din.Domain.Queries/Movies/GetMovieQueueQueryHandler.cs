using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieQueueQueryHandler : IRequestHandler<GetMovieQueueQuery, IEnumerable<RadarrQueue>>
    {
        private readonly IRadarrClient _client;

        public GetMovieQueueQueryHandler(IRadarrClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<RadarrQueue>> Handle(GetMovieQueueQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetQueueAsync(cancellationToken);
        }
    }
}
