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
        public Filters Filters { get; }
        public bool Plex { get; }
        public bool Poster { get; }

        public GetMoviesQuery(QueryParameters<RadarrMovie> queryParameters, Filters filters, bool plex, bool poster) :
            base(queryParameters)
        {
            Filters = filters;
            Plex = plex;
            Poster = poster;
        }
    }
}