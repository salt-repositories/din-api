using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class MoviePoller : BackgroundTask
    {
        private readonly ContentPollingQueue _contentPollingQueue;
        private readonly IMapper _mapper;
        private readonly IRadarrClient _radarrClient;
        private readonly Container _container;

        public MoviePoller(ILogger<MoviePoller> logger, ContentPollingQueue contentPollingQueue, IMapper mapper,
            IRadarrClient radarrClient, Container container) : base(nameof(MoviePoller), logger)
        {
            _contentPollingQueue = contentPollingQueue;
            _mapper = mapper;
            _radarrClient = radarrClient;
            _container = container;
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                Logger.LogInformation("Start polling movies");

                var repository = _container.GetInstance<IMovieRepository>();
                IList<RadarrMovie> externalMovies = new List<RadarrMovie>();

                try
                {
                    externalMovies = (await _radarrClient.GetMoviesAsync(cancellationToken)).ToList();
                }
                catch (HttpClientException exception)
                {
                    Logger.LogError(exception, exception.Message);
                }

                var storedMovies = await repository.GetMovies(null, null, cancellationToken);
                var moviesToAdd = new ConcurrentBag<Movie>();

                Parallel.ForEach(externalMovies, movie =>
                {
                    var storedMovie = storedMovies.FirstOrDefault(m => m.SystemId.Equals(movie.SystemId));

                    if (storedMovie == null)
                    {
                        storedMovie = _mapper.Map<Movie>(movie);
                        moviesToAdd.Add(storedMovie);
                        _contentPollingQueue.Enqueue(storedMovie);

                        return;
                    }

                    if (!storedMovie.Downloaded && movie.Downloaded)
                    {
                        storedMovie.Downloaded = true;
                    }
                });

                try
                {
                    await repository.InsertMultipleAsync(
                        moviesToAdd.GroupBy(m => m.SystemId).Select(group => group.First()).ToList(), cancellationToken);
                    await repository.SaveAsync(cancellationToken);

                    Logger.LogInformation($"Polled {moviesToAdd.Count} new movies");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception, "Polling movies insert error");
                }
            }
        }
    }
}