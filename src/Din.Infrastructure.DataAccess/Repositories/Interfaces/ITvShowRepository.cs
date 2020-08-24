using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface ITvShowRepository : IBaseRepository
    {
        Task<List<TvShow>> GetTvShows(QueryParameters queryParameters, TvShowFilters filters,
            CancellationToken cancellationToken);
        Task<List<TvShow>> GetTvShowsWithNoPoster(CancellationToken cancellationToken);
        Task<List<TvShow>> GetTvShowsWithNoPlexUrl(CancellationToken cancellationToken);
        Task<List<Episode>> GetTvShowEpisodesByDateRange((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken);
        Task<List<Episode>> GetTvShowEpisodes(Guid id, CancellationToken cancellationToken);
        Task<TvShow> GetTvShowById(Guid id, CancellationToken cancellationToken);
        Task<TvShow> GetTvShowBySystemId(int id, CancellationToken cancellationToken);
        Task AddMultipleEpisodes(IEnumerable<Episode> episodes, CancellationToken cancellationToken);
        int Count(ContentFilters filters);
        int CountTvShowEpisodes(Guid id);
    }
}
