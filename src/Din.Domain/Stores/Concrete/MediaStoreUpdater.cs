using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR.Pipeline;

namespace Din.Domain.Stores.Concrete
{
    public class MediaStoreUpdater<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IMediaAdditionRequest
    {
        private readonly IMediaStore _store;

        public MediaStoreUpdater(IMediaStore store)
        {
            _store = store;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (response.GetType() == typeof(RadarrMovie))
            {
                _store.AddMovie(response as RadarrMovie);
            }

            if (response.GetType() == typeof(SonarrTvShow))
            {
                _store.AddTvShow(response as SonarrTvShow);
            }

            return Task.CompletedTask;
        }
    }
}
