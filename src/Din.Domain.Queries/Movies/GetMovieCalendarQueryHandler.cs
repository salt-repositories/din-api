using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieCalendarQueryHandler : IRequestHandler<GetMovieCalendarQuery, List<Movie>>
    {
        private readonly IMovieRepository _repository;

        public GetMovieCalendarQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Movie>> Handle(GetMovieCalendarQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetMoviesByDateRange(request.DateRange, cancellationToken);
        }
    }
}