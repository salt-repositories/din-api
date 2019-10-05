using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQuery : RequestWithQueryParameters<SonarrTvShow>, IContentRetrievalRequest, IActivatedRequest, IRequest<QueryResult<SonarrTvShow>>
    {
        public Filters Filters { get; }
        public GetTvShowsQuery(QueryParameters<SonarrTvShow> queryParameters, Filters filters) : base(queryParameters)
        {
            Filters = filters;
        }
    }
}
