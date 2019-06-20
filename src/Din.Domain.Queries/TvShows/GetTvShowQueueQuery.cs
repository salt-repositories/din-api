using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowQueueQuery : IActivatedRequest, IRequest<IEnumerable<SonarrQueue>>
    {
    }
}
