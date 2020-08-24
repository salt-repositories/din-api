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
    public class PosterUrlPoller : IBackgroundTask
    {
        private readonly Container _container;
        private readonly ILogger<PosterUrlPoller> _logger;

        public PosterUrlPoller(Container container, ILogger<PosterUrlPoller> logger)
        {
            _container = container;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Polling for new posters");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var tvShowRepository = _container.GetInstance<ITvShowRepository>();
                var movieRepository = _container.GetInstance<IMovieRepository>();
                var posterHelper = _container.GetInstance<IPosterHelper>();
            
                var tvShows = await tvShowRepository.GetTvShowsWithNoPoster(cancellationToken);
                await posterHelper.GetPosters(tvShows, cancellationToken);
                await tvShowRepository.SaveAsync(cancellationToken);
            
                var movies = await movieRepository.GetMoviesWithNoPoster(cancellationToken);
                await posterHelper.GetPosters(movies, cancellationToken);
                await movieRepository.SaveAsync(cancellationToken);
            }

            _logger.LogInformation("Finished polling for new posters");
        }
    }
}
