using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowsQueryHandler : IRequestHandler<GetTvShowsQuery, QueryResult<TvShow>>
    {
        private readonly ITvShowRepository _repository;

        public GetTvShowsQueryHandler(ITvShowRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<TvShow>> Handle(GetTvShowsQuery request, CancellationToken cancellationToken)
        {
            var tvShows = await _repository.GetTvShows(request.QueryParameters, request.Filters, cancellationToken);
            var count = _repository.Count(request.Filters);

            return new QueryResult<TvShow>(tvShows, count);
        }
    }
}