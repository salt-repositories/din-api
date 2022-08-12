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

            var externalTvShows = await GetExternalTvShowsAsync(scope, cancellationToken);
            await CheckGenresAsync(scope, externalTvShows, cancellationToken);

            var storedTvShows = await repository.GetTvShows(null, null, cancellationToken);
            var tvShowsToAdd = GetTvShowsToAdd(scope, repository, externalTvShows, storedTvShows).ToList();
            
            await repository.InsertMultipleAsync(tvShowsToAdd, cancellationToken);
            await repository.SaveAsync(cancellationToken);
            
            Logger.LogInformation($"Added {tvShowsToAdd.Count} new tv shows");
            
            storedTvShows = await repository.GetTvShows(null, null, cancellationToken);
            var episodesToAdd = (await GetEpisodesToAddAsync(scope, repository, storedTvShows, cancellationToken)).ToList();
            
            await repository.AddMultipleEpisodes(episodesToAdd, cancellationToken);
            await repository.SaveAsync(cancellationToken);
            
            Logger.LogInformation($"Added {episodesToAdd.Count} new episodes");
        }

        private async Task<ICollection<SonarrTvShow>> GetExternalTvShowsAsync(Scope scope, CancellationToken cancellationToken)
        {
            var sonarrClient = scope.GetInstance<ISonarrClient>();
            return (await sonarrClient.GetTvShowsAsync(cancellationToken)).ToList();
        }

        private async Task CheckGenresAsync(Scope scope, IEnumerable<SonarrTvShow> externalTvShows, CancellationToken cancellationToken)
        {
            var repository = scope.GetInstance<IGenreRepository>();
            var currentGenres = await repository.GetGenresAsync(cancellationToken);
            var genres = externalTvShows.SelectMany(x => x.Genres).Distinct();
            var genresToAdd = genres.Where(name => !currentGenres.Select(x => x.Name).Contains(name));

            foreach (var genre in genresToAdd)
            {
                repository.Insert(new Genre
                {
                    Name = genre
                });
            }

            await repository.SaveAsync(cancellationToken);
        }

        private IEnumerable<TvShow> GetTvShowsToAdd(Scope scope, ITvShowRepository repository, IEnumerable<SonarrTvShow> externalTvShows, ICollection<TvShow> storedTvShows)
        {
            var genreRepository = scope.GetInstance<IGenreRepository>();
            
            // var externalSystemIds = externalTvShows.Select(x => x.SystemId);
            // var storedSystemIds = storedTvShows.Select(x => x.SystemId);
            // var tvShowsToAdd = externalSystemIds.Where(id => !storedSystemIds.Contains(id));
            //
            // foreach (var tvShow in tvShowsToAdd)
            // {
            //     yield return _mapper.Map<TvShow>(tvShow);
            // }
            
            foreach (var tvShow in externalTvShows)
            {
                var storedTvShow = storedTvShows.FirstOrDefault(t => t.SystemId.Equals(tvShow.SystemId));
                
                if (storedTvShow == null)
                {
                    storedTvShow = _mapper.Map<TvShow>(tvShow);
                    storedTvShow.Genres = tvShow.Genres
                        .Select(genre => new TvShowGenre {Genre = genreRepository.GetGenreByName(genre)})
                        .ToList();
                    
                    yield return storedTvShow;
                }

                if (storedTvShow.Status.Equals("continuing") &&
                    storedTvShow.EpisodeCount != tvShow.EpisodeCount ||
                    storedTvShow.SeasonCount != tvShow.SeasonCount)
                {
                    repository.Update(_mapper.Map(tvShow, storedTvShow));
                }
            }
        }

        private async Task<IEnumerable<Episode>> GetEpisodesToAddAsync(Scope scope, ITvShowRepository repository, IEnumerable<TvShow> tvShows, CancellationToken cancellationToken)
        {
            var sonarrClient = scope.GetInstance<ISonarrClient>();
            var episodesToAdd = new ConcurrentBag<Episode>();

            foreach (var tvShow in tvShows)
            {
                var storedEpisodes = await repository.GetTvShowEpisodes(tvShow.Id, cancellationToken);
                var externalEpisodes = await sonarrClient.GetTvShowEpisodes(tvShow.SystemId, cancellationToken);

                Parallel.ForEach(externalEpisodes, async externalEpisode =>
                {
                    var storedEpisode = GetStoredEpisode(storedEpisodes, externalEpisode, tvShow);
                    
                    if (storedEpisode == null)
                    {
                        var episode = _mapper.Map<Episode>(externalEpisode);
                        episode.TvShow = tvShow;
                        episodesToAdd.Add(episode);
                        
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
                });
            }

            return episodesToAdd;
        }
        
        private Episode? GetStoredEpisode(IReadOnlyCollection<Episode> storedEpisodes, SonarrEpisode externalEpisode, TvShow tvShow)
        {
            Episode storedEpisode;

            try
            {
                storedEpisode = storedEpisodes.SingleOrDefault(episode =>
                    tvShow.SystemId == externalEpisode.SeriesId &&
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
    }
}