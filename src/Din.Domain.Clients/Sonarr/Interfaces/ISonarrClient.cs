using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Domain.Clients.Sonarr.Interfaces
{
    public interface ISonarrClient
    {
        Task<IEnumerable<SonarrTvShow>> GetTvShowsAsync(CancellationToken cancellationToken);
        Task<SonarrTvShow> GetTvShowByIdAsync(int id, CancellationToken cancellationToken);
        Task<SonarrTvShow> AddTvShowAsync(SonarrTvShowRequest tvShow, CancellationToken cancellationToken);
        Task<SonarrCalendar> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken);
        Task<SonarrQueue> GetQueueAsync(CancellationToken cancellationToken);
    }
}
