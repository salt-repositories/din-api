using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<SonarrTvShow>
    {
        public int Id { get; }
        public bool Plex { get; }
        public bool Poster { get; }

        public GetTvShowByIdQuery(int id, bool plex, bool poster)
        {
            Id = id;
            Plex = plex;
            Poster = poster;
        }
    }
}
