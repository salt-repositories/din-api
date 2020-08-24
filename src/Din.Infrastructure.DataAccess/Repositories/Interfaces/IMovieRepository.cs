using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IMovieRepository : IBaseRepository
    {
        Task<List<Movie>> GetMovies(QueryParameters queryParameters, MovieFilters filters,
            CancellationToken cancellationToken);
        Task<List<Movie>> GetMoviesWithNoPoster(CancellationToken cancellationToken);
        Task<List<Movie>> GetMoviesWithNoPlexUrl(CancellationToken cancellationToken);
        Task<List<Movie>> GetMoviesByDateRange((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken);
        Task<Movie> GetMovieById(Guid id, CancellationToken cancellationToken);
        Task<Movie> GetMovieBySystemId(int id, CancellationToken cancellationToken);
        int Count(ContentFilters filters);
    }
}
