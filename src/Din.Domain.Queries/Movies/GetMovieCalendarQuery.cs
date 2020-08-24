using System;
using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieCalendarQuery : IActivatedRequest, IRequest<List<Movie>>
    {
        public (DateTime from, DateTime till) DateRange { get; }

        public GetMovieCalendarQuery((DateTime from, DateTime till) dateRange)
        {
            DateRange = dateRange;
        }
    }
}
