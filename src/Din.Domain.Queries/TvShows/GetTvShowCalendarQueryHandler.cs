using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQueryHandler : IRequestHandler<GetTvShowCalendarQuery, IEnumerable<SonarrCalendar>>
    {
        private readonly ISonarrClient _client;

        public GetTvShowCalendarQueryHandler(ISonarrClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<SonarrCalendar>> Handle(GetTvShowCalendarQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetCalendarAsync(request.DateRange, cancellationToken);
        }
    }
}
