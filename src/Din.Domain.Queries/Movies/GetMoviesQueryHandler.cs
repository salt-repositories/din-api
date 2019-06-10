using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<RadarrMovie>>
    {
        private readonly IContentStore<RadarrMovie> _store;

        public GetMoviesQueryHandler(IContentStore<RadarrMovie> store)
        {
            _store = store;
        }

        public Task<IEnumerable<RadarrMovie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult((IEnumerable<RadarrMovie>) _store.GetAll());
        }
    }
}
