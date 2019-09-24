using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieQueueQuery : IActivatedRequest, IRequest<IEnumerable<RadarrQueue>>
    {
    }
}
