using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieQueueQuery : IRequest<IEnumerable<RadarrQueue>>
    {
    }
}
