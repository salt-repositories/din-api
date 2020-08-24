using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQuery : RequestWithQueryParameters, IActivatedRequest, IRequest<QueryResult<TvShow>>
    {
        public TvShowFilters Filters { get; }
        public GetTvShowsQuery(QueryParameters queryParameters, TvShowFilters filters) : base(queryParameters)
        {
            Filters = filters;
        }
    }
}
