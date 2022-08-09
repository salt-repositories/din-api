using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
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

            var tvShowRepository = scope.GetInstance<ITvShowRepository>();
            var movieRepository = scope.GetInstance<IMovieRepository>();

            var tvShowPosterTask = GetPosters(
                await tvShowRepository.GetTvShowsWithNoPoster(cancellationToken),
                tvShowRepository,
                cancellationToken
            );

            var moviePosterTask = GetPosters(
                await movieRepository.GetMoviesWithNoPoster(cancellationToken),
                movieRepository,
                cancellationToken
            );

            await Task.WhenAll(tvShowPosterTask, moviePosterTask);
            Logger.LogInformation("Finished polling for new posters");
        }

        private async Task GetPosters<TEntity, TRepository>(List<TEntity> content, TRepository repository,
            CancellationToken cancellationToken)
            where TEntity : class, IContent
            where TRepository : IBaseRepository
        {
            await EnumerateAndDoWorkAsync(content.TakeWhile(item => string.IsNullOrEmpty(item.PosterPath)), async item =>
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