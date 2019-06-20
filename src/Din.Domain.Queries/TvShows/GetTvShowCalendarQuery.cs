using System;
using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQuery : IActivatedRequest, IRequest<IEnumerable<SonarrCalendar>>
    {
        public (DateTime from, DateTime till) DateRange { get; }

        public GetTvShowCalendarQuery((DateTime from, DateTime till) dateRange)
        {
            DateRange = dateRange;
        }
    }
}
