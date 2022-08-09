using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class UpdateTvShowDatabase : BackgroundTask
    {
        private readonly ContentPollingQueue _contentPollingQueue;
        private readonly IMapper _mapper;

        protected override IEnumerable<string> Triggers => new[]
        {
            nameof(UpdateTvShowEpisodeDatabase)
        };

        public UpdateTvShowDatabase(
            Container container,
            ILogger<UpdateTvShowDatabase> logger,
            ContentPollingQueue contentPollingQueue,
            IMapper mapper) : base(container, logger, nameof(UpdateTvShowDatabase))
        {
            _contentPollingQueue = contentPollingQueue;
            _mapper = mapper;
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Start polling tv shows");

            var repository = scope.GetInstance<ITvShowRepository>();
            var sonarrClient = scope.GetInstance<ISonarrClient>();

            IList<SonarrTvShow> externalTvShows;

            try
            {
                externalTvShows = (await sonarrClient.GetTvShowsAsync(cancellationToken)).ToList();
            }
            catch (HttpClientException exception)
            {
                Logger.LogError(exception, exception.Message);
                return;
            }

            var storedTvShows = await repository.GetTvShows(null, null, cancellationToken);
            var tvShowsToAdd = new ConcurrentBag<TvShow>();

            AmountOfWork = externalTvShows.Count - 10;

            foreach (var tvShow in externalTvShows)
            {
                var storedTvShow = storedTvShows.FirstOrDefault(t => t.SystemId.Equals(tvShow.SystemId));
                
                IncreaseProgress(1);

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
                var genres = tvShowsToAdd.SelectMany(x => x.Genres.Select(x => x.Genre));

                genres = genres.DistinctBy(x => x.Name);

                await repository.InsertMultipleAsync(genres, cancellationToken);
                
                await repository.InsertMultipleAsync(tvShowsToAdd, cancellationToken);
                await repository.SaveAsync(cancellationToken);
                
                IncreaseProgress(10);

                Logger.LogInformation($"Polled {tvShowsToAdd.Count} new tv shows");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Polling tvShows insert error");
            }
        }
    }
}