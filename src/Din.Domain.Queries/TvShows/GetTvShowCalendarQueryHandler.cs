using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQueryHandler : CalendarQueryHandler<SonarrTvShow>,
        IRequestHandler<GetTvShowCalendarQuery, IEnumerable<SonarrCalendar>>
    {
        private readonly ISonarrClient _client;

        public GetTvShowCalendarQueryHandler(IPlexPosterStore store, IPlexHelper plexHelper, IPosterHelper posterHelper,
            ISonarrClient client) : base(store, plexHelper, posterHelper)
        {
            _client = client;
        }

        public async Task<IEnumerable<SonarrCalendar>> Handle(GetTvShowCalendarQuery request,
            CancellationToken cancellationToken)
        {
            var collection = (await _client.GetCalendarAsync(request.DateRange, cancellationToken)).ToList();

            await RetrieveAdditionalData(collection.Select(i => i.TvShow).ToList(), cancellationToken);

            return collection;
        }
    }
}