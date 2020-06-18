using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundTasks.Interfaces;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Stores.Interfaces;
using Microsoft.Extensions.Logging;

namespace Din.Domain.BackgroundTasks.Concrete
{
    public class ContentStoreRefresher : IBackgroundTask
    {
        private readonly ILogger<ContentStoreRefresher> _logger;
        private readonly IContentStore<RadarrMovie> _movieStore;
        private readonly IContentStore<SonarrTvShow> _tvShowStore;
        private readonly IRadarrClient _radarrClient;
        private readonly ISonarrClient _sonarrClient;

        public ContentStoreRefresher(ILogger<ContentStoreRefresher> logger, IContentStore<RadarrMovie> movieStore, IContentStore<SonarrTvShow> tvShowStore, IRadarrClient radarrClient, ISonarrClient sonarrClient)
        {
            _logger = logger;
            _movieStore = movieStore;
            _tvShowStore = tvShowStore;
            _radarrClient = radarrClient;
            _sonarrClient = sonarrClient;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            try
            {
                if (_movieStore.ShouldUpdate())
                {
                    _logger.LogInformation("Refreshing movie store");
                    _movieStore.Set((await _radarrClient.GetMoviesAsync(cancellationToken)).ToList());
                }

                if (_tvShowStore.ShouldUpdate())
                {
                    _logger.LogInformation("Refreshing tv show store");
                    _tvShowStore.Set((await _sonarrClient.GetTvShowsAsync(cancellationToken)).ToList());
                }
            }
            catch (HttpClientException exception)
            {
                _logger.LogError($"Error refreshing content store: ({exception.Message})");
            }
        }
    }
}
