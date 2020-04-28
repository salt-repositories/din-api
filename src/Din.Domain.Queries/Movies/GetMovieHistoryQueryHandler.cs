using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieHistoryQueryHandler : IRequestHandler<GetMovieHistoryQuery, HistoryResult<RadarrHistoryRecord>>
    {
        private readonly IRadarrClient _client;

        public GetMovieHistoryQueryHandler(IRadarrClient client)
        {
            _client = client;
        }

        public Task<HistoryResult<RadarrHistoryRecord>> Handle(GetMovieHistoryQuery request, CancellationToken cancellationToken)
        {
            return _client.GetMovieHistoryAsync(request.Id, cancellationToken);
        }
    }
}
