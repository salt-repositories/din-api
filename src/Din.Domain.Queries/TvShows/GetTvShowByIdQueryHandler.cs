using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQueryHandler : IRequestHandler<GetTvShowByIdQuery, TvShow>
    {
        private readonly ITvShowRepository _repository;

        public GetTvShowByIdQueryHandler(ITvShowRepository repository)
        {
            _repository = repository;
        }

        public Task<TvShow> Handle(GetTvShowByIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetTvShowById(request.Id, cancellationToken);
        }
    }
}