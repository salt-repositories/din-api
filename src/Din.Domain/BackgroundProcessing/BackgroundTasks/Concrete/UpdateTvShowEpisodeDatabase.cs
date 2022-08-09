using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class UpdateTvShowEpisodeDatabase : BackgroundTask
    {
        private readonly IMapper _mapper;

        public UpdateTvShowEpisodeDatabase(
            Container container,
            ILogger<UpdateTvShowEpisodeDatabase> logger,
            IMapper mapper) : base(container, logger, nameof(UpdateTvShowEpisodeDatabase))
        {
            _mapper = mapper;
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Start polling tv show episodes");

            var repository = scope.GetInstance<ITvShowRepository>();
            var sonarrClient = scope.GetInstance<ISonarrClient>();
            
            var episodesToAdd = new List<Episode>();
            var currentTvShows = await repository.GetTvShows(null, null, cancellationToken);

            AmountOfWork = currentTvShows.Count - 10;

            foreach (var tvShow in currentTvShows)
            {
                var storedEpisodes = await repository.GetTvShowEpisodes(tvShow.Id, cancellationToken);
                var externalEpisodes = await sonarrClient.GetTvShowEpisodes(tvShow.SystemId, cancellationToken);

                foreach (var externalEpisode in externalEpisodes)
                {
                    var storedEpisode = GetStoredEpisode(storedEpisodes, externalEpisode, tvShow);
                    VerifyAndAddEpisode(storedEpisode, externalEpisode, episodesToAdd);
                }
                
                IncreaseProgress(1);
            }

            try
            {
                await repository.AddMultipleEpisodes(episodesToAdd, cancellationToken);
                await repository.SaveAsync(cancellationToken);
                    
                Logger.LogInformation($"Polled {episodesToAdd.Count} new tv show episodes");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Polling tv shows episodes insert error");
            }
            
            IncreaseProgress(10);

            Logger.LogInformation("Finished polling tv show episodes");
        }

        private Episode? GetStoredEpisode(IReadOnlyCollection<Episode> storedEpisodes, SonarrEpisode externalEpisode, TvShow tvShow)
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
                Logger.LogInformation(
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