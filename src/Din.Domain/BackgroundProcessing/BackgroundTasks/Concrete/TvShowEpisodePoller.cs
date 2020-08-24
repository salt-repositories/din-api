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
        private readonly Container _container;
        private readonly IMapper _mapper;
        private readonly ILogger<TvShowEpisodePoller> _logger;

        public TvShowEpisodePoller(Container container, IMapper mapper, ILogger<TvShowEpisodePoller> logger)
        {
            _container = container;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start polling tv show episodes");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var repository = _container.GetInstance<ITvShowRepository>();
                var client = _container.GetInstance<ISonarrClient>();

                var episodesToAdd = new List<Episode>();

                foreach (var tvShow in (await repository.GetTvShows(null, null, cancellationToken)).Where(tvShow => tvShow.TotalEpisodeCount != repository.CountTvShowEpisodes(tvShow.Id)))
                {
                    var storedEpisodes = await repository.GetTvShowEpisodes(tvShow.Id, cancellationToken);
                    var externalEpisodes = await client.GetTvShowEpisodes(tvShow.SystemId, cancellationToken);

                    episodesToAdd.AddRange(externalEpisodes
                        .Where(episode => storedEpisodes.FirstOrDefault(e =>
                                              e.SeasonNumber.Equals(episode.SeasonNumber) &&
                                              e.EpisodeNumber.Equals(episode.EpisodeNumber)) == null)
                        .Select(episode => _mapper.Map<Episode>(episode)));
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
            }

            _logger.LogInformation("Finished polling tv show episodes");
        }
    }
}
