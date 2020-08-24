using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly IMovieRepository _repository;

        public GetMovieByIdQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetMovieById(request.Id, cancellationToken);
        }
    }
}