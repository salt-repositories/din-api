using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Clients.Sonarr.Interfaces;
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
                        Episode storedEpisode;

                        try
                        {
                            storedEpisode = storedEpisodes.SingleOrDefault(episode =>
                                episode.SeasonNumber == externalEpisode.SeasonNumber &&
                                episode.EpisodeNumber == externalEpisode.EpisodeNumber);
                        }
                        catch (InvalidOperationException)
                        {
                            _logger.LogInformation(
                                "Found multiple hits for episode: S{seasonNumber}E{episodeNumber} {name} of tvshow: {tvshow}",
                                externalEpisode.SeasonNumber, externalEpisode.EpisodeNumber, externalEpisode.Title,
                                tvShow.Title);

                            storedEpisode = storedEpisodes.FirstOrDefault(episode =>
                                episode.SeasonNumber == externalEpisode.SeasonNumber &&
                                episode.EpisodeNumber == externalEpisode.EpisodeNumber);
                        }

                        if (storedEpisode != null && !storedEpisode.HasFile)
                        {
                            storedEpisode.HasFile = externalEpisode.HasFile;
                        }

                        episodesToAdd.Add(_mapper.Map<Episode>(externalEpisode));
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
    }
}