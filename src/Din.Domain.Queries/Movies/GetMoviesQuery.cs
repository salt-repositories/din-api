using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : RequestWithQueryParameters<RadarrMovie>, IContentRetrievalRequest, IActivatedRequest, IRequest<QueryResult<RadarrMovie>>
    {
        public string Title { get; }
        public GetMoviesQuery(QueryParameters<RadarrMovie> queryParameters, string title) : base(queryParameters)
        {
            Title = title;
        }
    }
}