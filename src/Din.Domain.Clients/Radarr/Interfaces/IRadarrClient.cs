using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Domain.Clients.Radarr.Interfaces
{
    public interface IRadarrClient
    {
        Task<T> GetMoviesAsync<T>(RadarrMovieQuery query, CancellationToken cancellationToken);
        Task<RadarrMovie> GetMovieByIdAsync(int id, CancellationToken cancellationToken);
        Task<RadarrMovie> AddMovieAsync(RadarrMovieRequest movie, CancellationToken cancellationToken);
        Task<IEnumerable<RadarrCalendar>> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken);
        Task<IEnumerable<RadarrQueue>> GetQueueAsync(CancellationToken cancellationToken);
    }
}
