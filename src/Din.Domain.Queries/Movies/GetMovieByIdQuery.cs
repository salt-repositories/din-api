using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQuery : IRequest<RadarrMovie>
    {
        public int Id { get; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
