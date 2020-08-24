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
    public class TvShowRepository : BaseRepository, ITvShowRepository
    {
        public TvShowRepository(DinContext context) : base(context)
        {
        }

        public Task<List<TvShow>> GetTvShows(QueryParameters queryParameters, TvShowFilters filters, CancellationToken cancellationToken)
        {
            return Context.TvShow
                .Include(t => t.Seasons)
                .Include(t => t.Genres)
                .ThenInclude(g => g.Genre)
                .Include(t => t.Ratings)
                .ToListAsync(queryParameters, filters, cancellationToken);
        }

        public Task<List<TvShow>> GetTvShowsWithNoPoster(CancellationToken cancellationToken)
        {
            return Context.TvShow.Where(t => string.IsNullOrEmpty(t.PosterPath)).ToListAsync(cancellationToken);
        }

        public Task<List<TvShow>> GetTvShowsWithNoPlexUrl(CancellationToken cancellationToken)
        {
            return Context.TvShow.Where(t => string.IsNullOrEmpty(t.PlexUrl)).ToListAsync(cancellationToken);
        }

        public Task<List<Episode>> GetTvShowEpisodesByDateRange((DateTime @from, DateTime till) dateRange, CancellationToken cancellationToken)
        {
            return Context.Episode
                .Where(t => t.AirDate >= dateRange.from && t.AirDate <= dateRange.till)
                .Include(t => t.TvShow)
                .ThenInclude(t => t.Ratings)
                .Include(t => t.TvShow)
                .ThenInclude(t => t.Genres)
                .ThenInclude(g => g.Genre)
                .ToListAsync(cancellationToken);
        }

        public Task<List<Episode>> GetTvShowEpisodes(Guid id, CancellationToken cancellationToken)
        {
            return Context.Episode
                .Where(e => e.TvShow.Id.Equals(id))
                .ToListAsync(cancellationToken);
        }

        public Task<TvShow> GetTvShowById(Guid id, CancellationToken cancellationToken)
        {
            return Context.TvShow
                .Include(t => t.Seasons)
                .Include(t => t.Genres)
                .ThenInclude(g => g.Genre)
                .Include(t => t.Ratings)
                .FirstOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
        }

        public Task<TvShow> GetTvShowBySystemId(int id, CancellationToken cancellationToken)
        {
            return Context.TvShow
                .Include(t => t.Seasons)
                .Include(t => t.Genres)
                .ThenInclude(g => g.Genre)
                .Include(t => t.Ratings)
                .FirstOrDefaultAsync(t => t.SystemId.Equals(id), cancellationToken);
        }

        public Task AddMultipleEpisodes(IEnumerable<Episode> episodes, CancellationToken cancellationToken)
        {
            return Context.Episode.AddRangeAsync(episodes, cancellationToken);
        }

        public int Count(ContentFilters filters)
        {
            IQueryable<TvShow> query = Context.Set<TvShow>();

            return query.ApplyFilters(filters).Count();
        }

        public int CountTvShowEpisodes(Guid id)
        {
            return Context.Episode.Count(e => e.TvShow.Id.Equals(id));
        }
    }
}
