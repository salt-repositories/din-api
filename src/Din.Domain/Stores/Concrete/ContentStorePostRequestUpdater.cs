using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR.Pipeline;

namespace Din.Domain.Stores.Concrete
{
    public class ContentStorePostRequestUpdater<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IContentAdditionRequest
    {
        private readonly IContentStore<RadarrMovie> _movieStore;
        private readonly IContentStore<SonarrTvShow> _tvShowStore;

        public ContentStorePostRequestUpdater(IContentStore<RadarrMovie> movieStore, IContentStore<SonarrTvShow> tvShowStore)
        {
            _movieStore = movieStore;
            _tvShowStore = tvShowStore;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (response.GetType() == typeof(RadarrMovie))
            {
                _movieStore.AddOne(response as RadarrMovie);
            }

            if (response.GetType() == typeof(SonarrTvShow))
            {
                _tvShowStore.AddOne(response as SonarrTvShow);
            }

            return Task.CompletedTask;
        }
    }
}
