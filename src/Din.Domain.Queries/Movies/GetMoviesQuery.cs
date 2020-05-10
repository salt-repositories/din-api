using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : RequestWithQueryParameters<RadarrMovie>, IContentRetrievalRequest, IActivatedRequest,
        IRequest<QueryResult<RadarrMovie>>
    {
        public ContentFilters Filters { get; }
        public ContentQueryParameters ContentQueryParameters { get; }

        public GetMoviesQuery(QueryParameters<RadarrMovie> queryParameters, ContentFilters filters, ContentQueryParameters contentQueryParameters) : base(queryParameters)
        {
            Filters = filters;
            ContentQueryParameters = contentQueryParameters;
        }
    }
}