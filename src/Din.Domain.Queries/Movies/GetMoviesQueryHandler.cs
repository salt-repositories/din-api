using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, QueryResult<Movie>>
    {
        private readonly IMovieRepository _repository;

        public GetMoviesQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetMovies(request.QueryParameters, request.Filters, cancellationToken);
            var count = _repository.Count(request.Filters);

            return new QueryResult<Movie>(movies, count);
        }
    }
}