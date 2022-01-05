using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Helpers.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class PlexUrlPoller : IBackgroundTask
    {
        private readonly ILogger<PlexUrlPoller> _logger;
        private readonly IPlexHelper _plexHelper;
        private readonly Container _container;

        public PlexUrlPoller(ILogger<PlexUrlPoller> logger, IPlexHelper plexHelper, Container container)
        {
            _logger = logger;
            _plexHelper = plexHelper;
            _container = container;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _logger.LogInformation("Polling for plex urls");

                var movieRepository = _container.GetInstance<IMovieRepository>();
                var tvShowRepository = _container.GetInstance<ITvShowRepository>();

                var movies = await movieRepository.GetMoviesWithNoPlexUrl(cancellationToken);
                await _plexHelper.CheckIsOnPlex(movies, cancellationToken);
                await movieRepository.SaveAsync(cancellationToken);
        
                var tvShows = await tvShowRepository.GetTvShowsWithNoPlexUrl(cancellationToken);
                await _plexHelper.CheckIsOnPlex(tvShows, cancellationToken);
                await tvShowRepository.SaveAsync(cancellationToken);

                _logger.LogInformation("Finished polling for plex url");
            }
        }
    }
}
