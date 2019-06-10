using System;
using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieCalendarQuery : IRequest<IEnumerable<RadarrMovie>>
    {
        public (DateTime from, DateTime till) DateRange { get; }

        public GetMovieCalendarQuery((DateTime from, DateTime till) dateRange)
        {
            DateRange = dateRange;
        }
    }
}
