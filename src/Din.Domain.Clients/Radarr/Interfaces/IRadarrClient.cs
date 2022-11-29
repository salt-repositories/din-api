﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Domain.Clients.Radarr.Interfaces
{
    public interface IRadarrClient
    {
        Task<IEnumerable<RadarrMovie>> GetMoviesAsync(CancellationToken cancellationToken);
        Task<RadarrMovie> GetMovieByIdAsync(int id, CancellationToken cancellationToken);
        Task<RadarrMovie> AddMovieAsync(RadarrMovieRequest movie, CancellationToken cancellationToken);
        Task<IEnumerable<RadarrMovie>> GetCalendarAsync((DateTime from, DateTime till) dateRange, CancellationToken cancellationToken);
        Task<IEnumerable<RadarrQueue>> GetQueueAsync(CancellationToken cancellationToken);
        Task<IEnumerable<RadarrHistoryRecord>> GetMovieHistoryAsync(int id, CancellationToken cancellationToken);
    }
}
