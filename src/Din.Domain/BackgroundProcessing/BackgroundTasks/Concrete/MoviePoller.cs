using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
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
    public class MoviePoller : IBackgroundTask
    {
        private readonly Container _container;
        private readonly ContentPollingQueue _contentPollingQueue;
        private readonly IMapper _mapper;
        private readonly ILogger<MoviePoller> _logger;

        public MoviePoller(Container container, ContentPollingQueue contentPollingQueue, IMapper mapper, ILogger<MoviePoller> logger)
        {
            _container = container;
            _contentPollingQueue = contentPollingQueue;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start polling movies");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var repository = _container.GetInstance<IMovieRepository>();
                var client = _container.GetInstance<IRadarrClient>();

                IList<RadarrMovie> externalMovies = new List<RadarrMovie>();

                try
                {
                    externalMovies = (await client.GetMoviesAsync(cancellationToken)).ToList();
                }
                catch (HttpClientException exception)
                {
                    _logger.LogError(exception, exception.Message);
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
                    await repository.InsertMultipleAsync(moviesToAdd, cancellationToken);
                    await repository.SaveAsync(cancellationToken);

                    _logger.LogInformation($"Polled {moviesToAdd.Count} new movies");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Polling movies insert error");
                }
            }
        }
    }
}
