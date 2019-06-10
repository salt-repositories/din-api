using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR.Pipeline;

namespace Din.Domain.Stores.Concrete
{
    public class ContentStoreUpdater<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IContentRetrievalRequest
    {
        private readonly IContentStore<RadarrMovie> _movieStore;
        private readonly IContentStore<SonarrTvShow> _tvShowStore;
        private readonly IRadarrClient _radarrClient;
        private readonly ISonarrClient _sonarrClient;

        public ContentStoreUpdater(IContentStore<RadarrMovie> movieStore, IContentStore<SonarrTvShow> tvShowStore,
            IRadarrClient radarrClient, ISonarrClient sonarrClient)
        {
            _movieStore = movieStore;
            _tvShowStore = tvShowStore;
            _radarrClient = radarrClient;
            _sonarrClient = sonarrClient;
        }
        
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (request.GetType().Name.Contains("Movie") && _movieStore.Content == null)
            {
                _movieStore.Set((await _radarrClient.GetMoviesAsync(cancellationToken)).ToList());
            }

            if (request.GetType().Name.Contains("TvShow") && _tvShowStore.Content == null)
            {
                _tvShowStore.Set((await _sonarrClient.GetTvShowsAsync(cancellationToken)).ToList());
            }
        }
    }
}