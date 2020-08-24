using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieBySystemIdQueryHandler : IRequestHandler<GetMovieBySystemIdQuery, Movie>
    {
        private readonly IMovieRepository _repository;

        public GetMovieBySystemIdQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task<Movie> Handle(GetMovieBySystemIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetMovieBySystemId(request.Id, cancellationToken);
        }
    }
}
