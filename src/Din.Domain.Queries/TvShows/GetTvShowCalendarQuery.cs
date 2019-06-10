using System;
using System.Collections.Generic;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQuery : IRequest<IEnumerable<SonarrTvShow>>
    {
        public (DateTime from, DateTime till) DateRange { get; }

        public GetTvShowCalendarQuery((DateTime from, DateTime till) dateRange)
        {
            DateRange = dateRange;
        }
    }
}
