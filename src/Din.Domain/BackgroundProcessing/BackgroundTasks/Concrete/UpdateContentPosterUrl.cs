using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Extensions;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TMDbLib.Client;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class UpdateContentPosterUrl : BackgroundTask
    {
        private readonly TMDbClient _tmDbClient;

        public UpdateContentPosterUrl(
            Container container,
            ILogger<UpdateContentPosterUrl> logger,
            ITmdbClientConfig config) : base(container, logger, nameof(UpdateContentPosterUrl))
        {
            _tmDbClient = new TMDbClient(config.Key);
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Polling for new posters");

            var tvShows = await scope.WithRepository<ITvShowRepository, List<TvShow>>(
                repository => repository.GetTvShowsWithNoPoster(cancellationToken)
            );
            var movies = await scope.WithRepository<IMovieRepository, List<Movie>>(
                repository => repository.GetMoviesWithNoPoster(cancellationToken)
            );

            var tvShowPosterTask = GetPosters<ITvShowRepository>(
                tvShows,
                scope,
                cancellationToken
            );

            var moviePosterTask = GetPosters<IMovieRepository>(
                movies,
                scope,
                cancellationToken
            );

            await Task.WhenAll(tvShowPosterTask, moviePosterTask);
            Logger.LogInformation("Finished polling for new posters");
        }

        private async Task GetPosters<TRepository>(
            IEnumerable<IContent> content,
            Scope scope,
            CancellationToken cancellationToken)
            where TRepository : class, IBaseRepository
        {
            if (scope.Container == null)
            {
                Logger.LogError("Scope has no parent container");
                return;
            }
            
            await using var innerScope = AsyncScopedLifestyle.BeginScope(scope.Container);
            var repository = innerScope.GetInstance<TRepository>();

            await EnumerateAndDoWorkAsync(content.TakeWhile(item => string.IsNullOrEmpty(item.PosterPath)),
                async item =>
                {
                    if (item.GetType() == typeof(Movie))
                    {
                        var response = await _tmDbClient.SearchMovieAsync(item.Title, 0, false,
                            Convert.ToInt32(item.Year),
                            null,
                            0,
                            cancellationToken);

                        if (response.Results.Any())
                        {
                            item.PosterPath = response.Results.First().PosterPath;
                        }
                    }
                    else
                    {
                        var response = await _tmDbClient.SearchTvShowAsync(item.Title, 0, false, 0, cancellationToken);

                        if (response.Results.Any())
                        {
                            item.PosterPath = response.Results.First().PosterPath;
                        }
                    }
                });

            await repository.SaveAsync(cancellationToken);
        }
    }
}