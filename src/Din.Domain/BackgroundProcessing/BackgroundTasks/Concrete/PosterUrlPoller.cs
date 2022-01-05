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
using SimpleInjector.Lifestyles;
using TMDbLib.Client;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class PosterUrlPoller : BackgroundTask
    {
        private readonly Container _container;
        private readonly TMDbClient _tmDbClient;

        public PosterUrlPoller(ILogger<PosterUrlPoller> logger, ITmdbClientConfig config, Container container) : base(nameof(PosterUrlPoller), logger)
        {
            _container = container;
            _tmDbClient = new TMDbClient(config.Key);
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                Logger.LogInformation("Polling for new posters");

                var tvShowRepository = _container.GetInstance<ITvShowRepository>();
                var movieRepository = _container.GetInstance<IMovieRepository>();

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
        }

        private async Task GetPosters<TEntity, TRepository>(IEnumerable<TEntity> content, TRepository repository, CancellationToken cancellationToken)
            where TEntity : class, IContent
            where TRepository : IBaseRepository 
        {
            foreach (var item in content)
            {
                if (!string.IsNullOrEmpty(item.PosterPath))
                {
                    // set progress
                    break;
                }

                // check for days not polled

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
            }

            await repository.SaveAsync(cancellationToken);
        }
    }
}