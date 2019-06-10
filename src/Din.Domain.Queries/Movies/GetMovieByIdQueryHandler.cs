using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, RadarrMovie>
    {
        private readonly IContentStore<RadarrMovie> _store;

        public GetMovieByIdQueryHandler(IContentStore<RadarrMovie> store)
        {
            _store = store;
        }

        public Task<RadarrMovie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_store.GetOneById(request.Id));
        }
    }
}