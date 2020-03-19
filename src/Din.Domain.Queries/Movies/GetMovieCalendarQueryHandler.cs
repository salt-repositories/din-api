using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Queries.Abstractions;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieCalendarQueryHandler : CalendarQueryHandler<RadarrMovie>, IRequestHandler<GetMovieCalendarQuery, IEnumerable<RadarrMovie>>
    {
        private readonly IRadarrClient _client;

        public GetMovieCalendarQueryHandler(IPlexPosterStore store, IPlexHelper plexHelper, IPosterHelper posterHelper, IRadarrClient client) : base(store, plexHelper, posterHelper)
        {
            _client = client;
        }

        public async Task<IEnumerable<RadarrMovie>> Handle(GetMovieCalendarQuery request,
            CancellationToken cancellationToken)
        {
            var collection = (await _client.GetCalendarAsync(request.DateRange, cancellationToken)).ToList();

            await RetrieveAdditionalData(collection, cancellationToken);

            return collection;
        }
    }
}