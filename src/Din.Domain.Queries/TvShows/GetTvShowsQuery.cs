using System.Collections.Generic;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQuery : IContentRetrievalRequest, IRequest<IEnumerable<SonarrTvShow>>
    {
    }
}
