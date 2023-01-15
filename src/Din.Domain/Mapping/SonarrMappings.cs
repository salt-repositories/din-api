using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;

namespace Din.Domain.Mapping;

public static class SonarrMappings
{
    public static TvShow ToEntity(this SonarrTvShow tvShow) => new()
    {
        SystemId = tvShow.SystemId,
        ImdbId = tvShow.ImdbId,
        Title = tvShow.Title,
        Overview = tvShow.Overview,
        Status = tvShow.Status,
        Downloaded = tvShow.Downloaded,
        HasFile = tvShow.HasFile,
        Year = tvShow.Year,
        Added = tvShow.Added,
        Ratings = tvShow.Ratings.ToEntity(),
        TvdbId = tvShow.TvdbId,
        SeasonCount = tvShow.SeasonCount,
        TotalEpisodeCount = tvShow.TotalEpisodeCount,
        EpisodeCount = tvShow.EpisodeCount,
        Network = tvShow.Network,
        AirTime = tvShow.AirTime
    };

    public static TvShow Update(this TvShow tvShow, SonarrTvShow sonarrTvShow)
    {
        tvShow.SystemId = sonarrTvShow.SystemId;
        tvShow.ImdbId = sonarrTvShow.ImdbId;
        tvShow.Title = sonarrTvShow.Title;
        tvShow.Overview = sonarrTvShow.Overview;
        tvShow.Status = sonarrTvShow.Status;
        tvShow.Downloaded = sonarrTvShow.Downloaded;
        tvShow.HasFile = sonarrTvShow.HasFile;
        tvShow.Year = sonarrTvShow.Year;
        tvShow.Added = sonarrTvShow.Added;
        tvShow.TvdbId = sonarrTvShow.TvdbId;
        tvShow.SeasonCount = sonarrTvShow.SeasonCount;
        tvShow.TotalEpisodeCount = sonarrTvShow.TotalEpisodeCount;
        tvShow.EpisodeCount = sonarrTvShow.EpisodeCount;
        tvShow.Network = sonarrTvShow.Network;
        tvShow.AirTime = sonarrTvShow.AirTime;

        return tvShow;
    }
    
    public static Episode ToEntity(this SonarrEpisode episode) => new()
    {
        SeasonNumber = episode.SeasonNumber,
        EpisodeNumber = episode.EpisodeNumber,
        Title = episode.Title,
        AirDate = episode.AirDate,
        Overview = episode.Overview,
        HasFile = episode.HasFile,
        Monitored = episode.Monitored    
    };
}