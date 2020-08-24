using System;
using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQuery : IActivatedRequest, IRequest<List<Episode>>
    {
        public (DateTime from, DateTime till) DateRange { get; }

        public GetTvShowCalendarQuery((DateTime from, DateTime till) dateRange)
        {
            DateRange = dateRange;
        }
    }
}
