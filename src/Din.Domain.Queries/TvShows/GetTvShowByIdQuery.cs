using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Models.Querying;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<SonarrTvShow>
    {
        public int Id { get; }
        public ContentFilters Filters { get; }
        public ContentQueryParameters ContentQueryParameters { get; }

        public GetTvShowByIdQuery(int id, ContentFilters filters, ContentQueryParameters contentQueryParameters)
        {
            Id = id;
            Filters = filters;
            ContentQueryParameters = contentQueryParameters;
        }
    }
}
