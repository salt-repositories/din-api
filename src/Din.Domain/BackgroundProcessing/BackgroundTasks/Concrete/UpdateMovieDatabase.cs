using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Mapping;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class UpdateMovieDatabase : BackgroundTask
    {
        private readonly ContentPollingQueue _contentPollingQueue;

        public UpdateMovieDatabase(
            Container container,
            ILogger<UpdateMovieDatabase> logger,
            ContentPollingQueue contentPollingQueue
        ) : base(container, logger, nameof(UpdateMovieDatabase))
        {
            _contentPollingQueue = contentPollingQueue;
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Start polling movies");

            var repository = scope.GetInstance<IMovieRepository>();
            var radarrClient = scope.GetInstance<IRadarrClient>();

            IList<RadarrMovie> externalMovies;

            try
            {
                externalMovies = (await radarrClient.GetMoviesAsync(cancellationToken)).ToList();
            }
            catch (HttpClientException exception)
            {
                Logger.LogError(exception, exception.Message);
                return;
            }

            var storedMovies = await repository.GetMovies(null, null, cancellationToken);
            var moviesToAdd = new ConcurrentBag<Movie>();

            AmountOfWork = externalMovies.Count - 10;

            Parallel.ForEach(externalMovies, movie =>
            {
                var storedMovie = storedMovies.SingleOrDefault(m => m.SystemId.Equals(movie.SystemId));

                IncreaseProgress(1);

                if (storedMovie == null)
                {
                    storedMovie = movie.ToEntity();
                    moviesToAdd.Add(storedMovie);
                }

                if (!storedMovie.Downloaded && movie.Downloaded)
                {
                    storedMovie.Downloaded = true;
                }

                if (storedMovie.Title != movie.Title)
                {
                    storedMovie.Update(movie);
                }
                
                storedMovie.Update(movie);

                _contentPollingQueue.Enqueue(storedMovie);
            });

            try
            {
                await repository.InsertMultipleAsync(
                    moviesToAdd.GroupBy(m => m.SystemId).Select(group => group.First()).ToList(),
                    cancellationToken
                );
                await repository.SaveAsync(cancellationToken);

                IncreaseProgress(10);

                Logger.LogInformation($"Polled {moviesToAdd.Count} new movies");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Polling movies insert error");
            }
        }
    }
}