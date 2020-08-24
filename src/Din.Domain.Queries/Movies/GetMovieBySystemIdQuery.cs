using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieBySystemIdQuery : IActivatedRequest, IRequest<Movie>
    {
        public int Id { get; }

        public GetMovieBySystemIdQuery(int id)
        {
            Id = id;
        }
    }
}
