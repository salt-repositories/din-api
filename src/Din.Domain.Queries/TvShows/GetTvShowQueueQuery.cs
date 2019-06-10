using System.Collections.Generic;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowQueueQuery : IRequest<IEnumerable<SonarrQueue>>
    {
    }
}
