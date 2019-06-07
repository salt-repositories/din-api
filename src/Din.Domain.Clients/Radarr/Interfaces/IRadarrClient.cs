using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Request;
using Din.Domain.Clients.Radarr.Response;

namespace Din.Domain.Clients.Radarr.Interfaces
{
    public interface IRadarrClient
    {
        Task<T> GetMoviesAsync<T>(RadarrMovieQuery query);
        Task<RadarrMovie> GetMovieByIdAsync(int id);
        Task<RadarrMovie> AddMovieAsync(RadarrMovieRequest movie);
        Task<IEnumerable<RadarrCalendar>> GetCalendarAsync((DateTime from, DateTime till) dateRange);
        Task<IEnumerable<RadarrQueue>> GetQueueAsync();
    }
}
