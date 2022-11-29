using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieHistoryQueryHandler : IRequestHandler<GetMovieHistoryQuery, IEnumerable<RadarrHistoryRecord>>
    {
        private readonly IRadarrClient _client;

        public GetMovieHistoryQueryHandler(IRadarrClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<RadarrHistoryRecord>> Handle(GetMovieHistoryQuery request, CancellationToken cancellationToken)
        {
            return _client.GetMovieHistoryAsync(request.Id, cancellationToken);
        }
    }
}
