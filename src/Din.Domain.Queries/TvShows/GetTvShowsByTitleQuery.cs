using System.Collections.Generic;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsByTitleQuery : IContentRetrievalRequest, IRequest<IEnumerable<SonarrTvShow>>
    {
        public string Title { get; }

        public GetTvShowsByTitleQuery(string title)
        {
            Title = title;
        }
    }
}
