using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Infrastructure.DataAccess.Querying;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(DinContext context) : base(context)
        {
        }

        public Task<List<Movie>> GetMovies(QueryParameters queryParameters, MovieFilters filters, CancellationToken cancellationToken)
        {
            return Context.Movie
                .Include(m => m.Ratings)
                .ToListAsync(queryParameters, filters, cancellationToken);
        }

        public Task<List<Movie>> GetMoviesWithNoPoster(CancellationToken cancellationToken)
        {
            return Context.Movie.Where(m => string.IsNullOrEmpty(m.PosterPath)).ToListAsync(cancellationToken);
        }

        public Task<List<Movie>> GetMoviesWithNoPlexUrl(CancellationToken cancellationToken)
        {
            return Context.Movie.Where(m => string.IsNullOrEmpty(m.PlexUrl)).ToListAsync(cancellationToken);
        }

        public Task<List<Movie>> GetMoviesByDateRange((DateTime @from, DateTime till) dateRange, CancellationToken cancellationToken)
        {
            return Context.Movie
                .Where(m => m.PhysicalRelease >= dateRange.from && m.PhysicalRelease <= dateRange.till)
                .Include(m => m.Ratings)
                .ToListAsync(cancellationToken);
        }

        public Task<Movie> GetMovieById(Guid id, CancellationToken cancellationToken)
        {
            return Context.Movie
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id.Equals(id), cancellationToken);
        }

        public Task<Movie> GetMovieBySystemId(int id, CancellationToken cancellationToken)
        {
            return Context.Movie
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.SystemId.Equals(id), cancellationToken);
        }

        public int Count(ContentFilters filters)
        {
            return Context.Movie.ApplyFilters(filters).Count();
        }
    }
}
