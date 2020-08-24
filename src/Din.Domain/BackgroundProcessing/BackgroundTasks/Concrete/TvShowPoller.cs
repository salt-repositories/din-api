using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class TvShowPoller : IBackgroundTask
    {
        private readonly Container _container;
        private readonly ContentPollingQueue _contentPollingQueue;
        private readonly IMapper _mapper;
        private readonly ILogger<TvShowPoller> _logger;

        public TvShowPoller(Container container, ContentPollingQueue contentPollingQueue, IMapper mapper, ILogger<TvShowPoller> logger)
        {
            _container = container;
            _contentPollingQueue = contentPollingQueue;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start polling tv shows");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var repository = _container.GetInstance<ITvShowRepository>();
                var client = _container.GetInstance<ISonarrClient>();

                IList<SonarrTvShow> externalTvShows = new List<SonarrTvShow>();

                try
                {
                    externalTvShows = (await client.GetTvShowsAsync(cancellationToken)).ToList();
                }
                catch (HttpClientException exception)
                {
                    _logger.LogError(exception, exception.Message);
                }

                var storedTvShows = await repository.GetTvShows(null, null, cancellationToken);
                var tvShowsToAdd = new ConcurrentBag<TvShow>();

                foreach (var tvShow in externalTvShows)
                {
                    var storedTvShow = storedTvShows.FirstOrDefault(t => t.SystemId.Equals(tvShow.SystemId));

                    if (storedTvShow == null)
                    {
                        storedTvShow = _mapper.Map<TvShow>(tvShow);
                        tvShowsToAdd.Add(storedTvShow);
                        _contentPollingQueue.Enqueue(storedTvShow);

                        continue;
                    }

                    if (storedTvShow.Status.Equals("continuing") &&
                        storedTvShow.EpisodeCount != tvShow.EpisodeCount ||
                        storedTvShow.SeasonCount != tvShow.SeasonCount)
                    {
                        repository.Update(_mapper.Map(tvShow, storedTvShow));
                    }
                }

                try
                {
                    await repository.InsertMultipleAsync(tvShowsToAdd, cancellationToken);
                    await repository.SaveAsync(cancellationToken);

                    _logger.LogInformation($"Polled {tvShowsToAdd.Count} new tv shows");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Polling tvShows insert error");
                }
            }
        }
    }
}
