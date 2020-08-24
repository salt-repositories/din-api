using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowBySystemIdQueryHandler : IRequestHandler<GetTvShowBySystemIdQuery, TvShow>
    {
        private readonly ITvShowRepository _repository;

        public GetTvShowBySystemIdQueryHandler(ITvShowRepository repository)
        {
            _repository = repository;
        }

        public Task<TvShow> Handle(GetTvShowBySystemIdQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetTvShowBySystemId(request.Id, cancellationToken);
        }
    }
}
