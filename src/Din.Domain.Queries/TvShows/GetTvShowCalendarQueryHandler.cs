using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowCalendarQueryHandler : IRequestHandler<GetTvShowCalendarQuery, List<Episode>>
    {
        private readonly ITvShowRepository _repository;

        public GetTvShowCalendarQueryHandler(ITvShowRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Episode>> Handle(GetTvShowCalendarQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetTvShowEpisodesByDateRange(request.DateRange, cancellationToken);
        }
    }
}