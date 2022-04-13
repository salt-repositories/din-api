﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class TvShowEpisodePoller : IBackgroundTask
    {
        private readonly ILogger<TvShowEpisodePoller> _logger;
        private readonly IMapper _mapper;
        private readonly ISonarrClient _client;
        private readonly Container _container;

        public TvShowEpisodePoller(ILogger<TvShowEpisodePoller> logger, IMapper mapper, ISonarrClient client,
            Container container)
        {
            _logger = logger;
            _mapper = mapper;
            _client = client;
            _container = container;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _logger.LogInformation("Start polling tv show episodes");

                var repository = _container.GetInstance<ITvShowRepository>();

                var episodesToAdd = new List<Episode>();
                var currentTvShows = await repository.GetTvShows(null, null, cancellationToken);

                foreach (var tvShow in currentTvShows)
                {
                    var storedEpisodes = await repository.GetTvShowEpisodes(tvShow.Id, cancellationToken);
                    var externalEpisodes = await _client.GetTvShowEpisodes(tvShow.SystemId, cancellationToken);

                    foreach (var externalEpisode in externalEpisodes)
                    {
                        var storedEpisode = GetStoredEpisode(storedEpisodes, externalEpisode, tvShow);
                        VerifyAndAddEpisode(storedEpisode, externalEpisode, episodesToAdd);
                    }
                }

                try
                {
                    await repository.AddMultipleEpisodes(episodesToAdd, cancellationToken);
                    await repository.SaveAsync(cancellationToken);
                    
                    _logger.LogInformation($"Polled {episodesToAdd.Count} new tv show episodes");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Polling tv shows episodes insert error");
                }

                _logger.LogInformation("Finished polling tv show episodes");
            }
        }

        private Episode? GetStoredEpisode(IReadOnlyCollection<Episode> storedEpisodes, SonarrEpisode externalEpisode, TvShow tvShow)
        {
            Episode storedEpisode;

            try
            {
                storedEpisode = storedEpisodes.SingleOrDefault(episode =>
                    episode.Title == externalEpisode.Title &&
                    episode.SeasonNumber == externalEpisode.SeasonNumber &&
                    episode.EpisodeNumber == externalEpisode.EpisodeNumber);
            }
            catch (InvalidOperationException)
            {
                _logger.LogInformation(
                    "Found multiple hits for episode: S{SeasonNumber}E{EpisodeNumber} {Name} of tvshow: {Tvshow}",
                    externalEpisode.SeasonNumber, externalEpisode.EpisodeNumber, externalEpisode.Title,
                    tvShow.Title);

                storedEpisode = storedEpisodes.FirstOrDefault(episode =>
                    episode.SeasonNumber == externalEpisode.SeasonNumber &&
                    episode.EpisodeNumber == externalEpisode.EpisodeNumber);
            }

            return storedEpisode;
        }

        private void VerifyAndAddEpisode(Episode? storedEpisode, SonarrEpisode externalEpisode, ICollection<Episode> episodesToAdd)
        {
            if (storedEpisode == null)
            {
                episodesToAdd.Add(_mapper.Map<Episode>(externalEpisode));
                return;
            }

            if (!storedEpisode.HasFile)
            {
                storedEpisode.HasFile = externalEpisode.HasFile;
            }

            if (storedEpisode.Title.Contains("TBA"))
            {
                storedEpisode.Title = externalEpisode.Title;
            }
        }
    }
}