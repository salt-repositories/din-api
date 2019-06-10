using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : IContentRetrievalRequest, IRequest<IEnumerable<RadarrMovie>>
    {
    }
}