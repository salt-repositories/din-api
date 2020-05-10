using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Models.Querying;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<RadarrMovie>
    {
        public int Id { get; }
        public ContentFilters Filters { get; }
        public ContentQueryParameters ContentQueryParameters { get; }

        public GetMovieByIdQuery(int id, ContentFilters filters, ContentQueryParameters contentQueryParameters)
        {
            Id = id;
            Filters = filters;
            ContentQueryParameters = contentQueryParameters;
        }
    }
}
