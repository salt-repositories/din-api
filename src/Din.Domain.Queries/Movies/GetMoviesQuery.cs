using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<IEnumerable<RadarrMovie>>
    {
    }
}