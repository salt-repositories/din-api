using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQuery : IContentRetrievalRequest, IActivatedRequest, IRequest<RadarrMovie>
    {
        public int Id { get; }
        public bool Plex { get; }

        public GetMovieByIdQuery(int id, bool plex)
        {
            Id = id;
            Plex = plex;
        }
    }
}
