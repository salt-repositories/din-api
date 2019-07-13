using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<SonarrTvShow>
    {
        public int Id { get; }

        public GetTvShowByIdQuery(int id)
        {
            Id = id;
        }
    }
}
