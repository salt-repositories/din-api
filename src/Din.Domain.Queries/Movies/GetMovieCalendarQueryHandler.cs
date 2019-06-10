using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieCalendarQueryHandler : IRequestHandler<GetMovieCalendarQuery, IEnumerable<RadarrMovie>>
    {
        private readonly IRadarrClient _client;

        public GetMovieCalendarQueryHandler(IRadarrClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<RadarrMovie>> Handle(GetMovieCalendarQuery request, CancellationToken cancellationToken)
        {
           return await _client.GetCalendarAsync(request.DateRange, cancellationToken);
        }
    }
}
