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
        private readonly Container _container;
        private readonly ILogger<PlexUrlPoller> _logger;

        public PlexUrlPoller(Container container, ILogger<PlexUrlPoller> logger)
        {
            _container = container;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Polling for plex urls");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var movieRepository = _container.GetInstance<IMovieRepository>();
                var tvShowRepository = _container.GetInstance<ITvShowRepository>();
                var plexHelper = _container.GetInstance<IPlexHelper>();
            
                var movies = await movieRepository.GetMoviesWithNoPlexUrl(cancellationToken);
                await plexHelper.CheckIsOnPlex(movies, cancellationToken);
                await movieRepository.SaveAsync(cancellationToken);
            
                var tvShows = await tvShowRepository.GetTvShowsWithNoPlexUrl(cancellationToken);
                await plexHelper.CheckIsOnPlex(tvShows, cancellationToken);
                await tvShowRepository.SaveAsync(cancellationToken);
            }

            _logger.LogInformation("Finished polling for plex url");
        }
    }
}
