using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQuery : IContentRetrievalRequest, IRequest<SonarrTvShow>
    {
        public int Id { get; }

        public GetTvShowByIdQuery(int id)
        {
            Id = id;
        }
    }
}
