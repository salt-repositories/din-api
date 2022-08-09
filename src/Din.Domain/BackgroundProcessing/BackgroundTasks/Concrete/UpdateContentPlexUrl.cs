using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Helpers.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class UpdateContentPlexUrl : BackgroundTask
    {
        public UpdateContentPlexUrl(
            Container container,
            ILogger<UpdateContentPlexUrl> logger) : base(container, logger, nameof(UpdateContentPlexUrl))
        {
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Update plex urls");

            var plexHelper = scope.GetInstance<IPlexHelper>();
            var movieRepository = scope.GetInstance<IMovieRepository>();
            var tvShowRepository = scope.GetInstance<ITvShowRepository>();

            var updateMoviePlexUrls = Task.Run(async () =>
            {
                var movies = await movieRepository.GetMoviesWithNoPlexUrl(cancellationToken);
                await plexHelper.CheckIsOnPlex(movies, cancellationToken);
            }, cancellationToken);

            var updateTvShowPlexUrls = Task.Run(async () =>
            {
                var tvShows = await tvShowRepository.GetTvShowsWithNoPlexUrl(cancellationToken);
                await plexHelper.CheckIsOnPlex(tvShows, cancellationToken);
            }, cancellationToken);

            await Task.WhenAll(updateMoviePlexUrls, updateTvShowPlexUrls);
            
            await movieRepository.SaveAsync(cancellationToken);
            await tvShowRepository.SaveAsync(cancellationToken);

            Logger.LogInformation("Finished updating plex urls");
        }
    }
}