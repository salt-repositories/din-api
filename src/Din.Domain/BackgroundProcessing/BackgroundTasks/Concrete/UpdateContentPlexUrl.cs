using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Extensions;
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

            var updateMoviePlexUrls = scope.WithRepository<IMovieRepository>(async repository =>
            {
                var movies = await repository.GetMoviesWithNoPlexUrl(cancellationToken);
                await plexHelper.CheckIsOnPlex(movies, cancellationToken);
                await repository.SaveAsync(cancellationToken);
            }); 
                
       
            // var updateTvShowPlexUrls = scope.WithRepository<ITvShowRepository>(async repository =>
            // {
            //     var tvShows = await repository.GetTvShowsWithNoPlexUrl(cancellationToken);
            //     await plexHelper.CheckIsOnPlex(tvShows, cancellationToken);
            //     await repository.SaveAsync(cancellationToken);
            // });

            await Task.WhenAll(updateMoviePlexUrls);
            
            Logger.LogInformation("Finished updating plex urls");
        }
    }
}