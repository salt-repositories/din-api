using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Mediatr.Interfaces;
using Din.Domain.Stores.Interfaces;
using MediatR.Pipeline;

namespace Din.Domain.Mediatr.Concrete
{
    public class ContentStorePreRequestUpdater<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IContentRetrievalRequest
    {
        private readonly IContentStore<RadarrMovie> _movieStore;
        private readonly IContentStore<SonarrTvShow> _tvShowStore;
        private readonly IRadarrClient _radarrClient;
        private readonly ISonarrClient _sonarrClient;

        public ContentStorePreRequestUpdater(IContentStore<RadarrMovie> movieStore, IContentStore<SonarrTvShow> tvShowStore,
            IRadarrClient radarrClient, ISonarrClient sonarrClient)
        {
            _movieStore = movieStore;
            _tvShowStore = tvShowStore;
            _radarrClient = radarrClient;
            _sonarrClient = sonarrClient;
        }
        
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (request.GetType().Name.Contains("Movie") && _movieStore.ShouldUpdate())
            {
                _movieStore.Set((await _radarrClient.GetMoviesAsync(cancellationToken)).ToList());
            }

            if (request.GetType().Name.Contains("TvShow") && _tvShowStore.ShouldUpdate())
            {
                _tvShowStore.Set((await _sonarrClient.GetTvShowsAsync(cancellationToken)).ToList());
            }
        }
    }
}