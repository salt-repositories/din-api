using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<IEnumerable<SonarrTvShow>>
    {
    }
}
