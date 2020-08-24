using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : RequestWithQueryParameters, IActivatedRequest, IRequest<QueryResult<Movie>>
    {
        public MovieFilters Filters { get; }

        public GetMoviesQuery(QueryParameters queryParameters, MovieFilters filters): base(queryParameters)
        {
            Filters = filters;
        }
    }
}