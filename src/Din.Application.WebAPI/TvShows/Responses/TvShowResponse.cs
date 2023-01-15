using System.Collections.Generic;
using System.Linq;
using Din.Application.WebAPI.Content.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using SonarrSeason = Din.Domain.Clients.Sonarr.Responses.Season;
using Season = Din.Domain.Models.Entities.Season;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public record TvShowResponse : ContentResponse
    {
        public int TvdbId { get; init; }
        public int SeasonCount { get; init; }
        public int TotalEpisodeCount { get; init; }
        public int EpisodeCount { get; init; }
        public string Network { get; init; }
        public string AirTime { get; init; }
        public IEnumerable<SeasonResponse> Seasons { get; init; }

        public static implicit operator TvShowResponse(TvShow tvShow) => new()
        {
            Id = tvShow.Id,
            SystemId = tvShow.SystemId,
            ImdbId = tvShow.ImdbId,
            Title = tvShow.Title,
            Overview = tvShow.Overview,
            Genres = tvShow.Genres.Select(genre => genre.Genre.Name),
            Status = tvShow.Status,
            Downloaded = tvShow.Downloaded,
            HasFile = tvShow.HasFile,
            Year = tvShow.Year,
            Added = tvShow.Added,
            PlexUrl = tvShow.PlexUrl,
            PosterPath = tvShow.PosterPath,
            Ratings = tvShow.Ratings,
            TvdbId = tvShow.TvdbId,
            SeasonCount = tvShow.SeasonCount,
            TotalEpisodeCount = tvShow.TotalEpisodeCount,
            EpisodeCount = tvShow.EpisodeCount,
            Network = tvShow.Network,
            AirTime = tvShow.AirTime,
            Seasons = tvShow.Seasons.Select(season => (SeasonResponse) season)
        };
        
        public static implicit operator TvShowResponse(SonarrTvShow tvShow) => new()
        {
            SystemId = tvShow.SystemId,
            ImdbId = tvShow.ImdbId,
            Title = tvShow.Title,
            Overview = tvShow.Overview,
            Genres = tvShow.Genres,
            Status = tvShow.Status,
            Downloaded = tvShow.Downloaded,
            HasFile = tvShow.HasFile,
            Year = tvShow.Year,
            Added = tvShow.Added,
            Ratings = tvShow.Ratings,
            TvdbId = tvShow.TvdbId,
            SeasonCount = tvShow.SeasonCount,
            TotalEpisodeCount = tvShow.TotalEpisodeCount,
            EpisodeCount = tvShow.EpisodeCount,
            Network = tvShow.Network,
            AirTime = tvShow.AirTime,
            Seasons = tvShow.Seasons.Select(season => (SeasonResponse) season)
        };
    }

    public record struct SeasonResponse
    {
        public int SeasonsNumber { get; init; }
        public int EpisodeCount { get; init; }
        public int TotalEpisodeCount { get; init; }

        public static implicit operator SeasonResponse(Season season) => new()
        {
            SeasonsNumber = season.SeasonsNumber,
            EpisodeCount = season.EpisodeCount,
            TotalEpisodeCount = season.TotalEpisodeCount
        };
        
        public static implicit operator SeasonResponse(SonarrSeason season) => new()
        {
            SeasonsNumber = season.SeasonsNumber,
            EpisodeCount = season.Statistics.EpisodeCount,
            TotalEpisodeCount = season.Statistics.TotalEpisodeCount
        };
    }
}