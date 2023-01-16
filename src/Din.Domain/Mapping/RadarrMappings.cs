using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Models.Entities;

namespace Din.Domain.Mapping;

public static class RadarrMappings
{
    public static Movie ToEntity(this RadarrMovie radarrMovie) => new()
    {
        SystemId = radarrMovie.SystemId,
        ImdbId = radarrMovie.ImdbId,
        Title = radarrMovie.Title,
        Overview = radarrMovie.Overview,
        Status = radarrMovie.Status,
        Downloaded = radarrMovie.Downloaded,
        HasFile = radarrMovie.HasFile,
        Year = radarrMovie.Year,
        Added = radarrMovie.Added,
        Ratings = radarrMovie.Ratings.ToEntity(),
        TmdbId = radarrMovie.TmdbId,
        Studio = radarrMovie.Studio,
        InCinemas = radarrMovie.InCinemas,
        PhysicalRelease = radarrMovie.PhysicalRelease,
        YoutubeTrailerId = radarrMovie.YoutubeTrailerId,
        AlternativeTitles = radarrMovie.AlternativeTitles
    };
    
    public static Movie Update(this Movie movie, RadarrMovie radarrMovie)
    {
        movie.SystemId = radarrMovie.SystemId;
        movie.ImdbId = radarrMovie.ImdbId;
        movie.Title = radarrMovie.Title;
        movie.Overview = radarrMovie.Overview;
        movie.Status = radarrMovie.Status;
        movie.Downloaded = radarrMovie.Downloaded;
        movie.HasFile = radarrMovie.HasFile;
        movie.Year = radarrMovie.Year;
        movie.Added = radarrMovie.Added;
        movie.Ratings = radarrMovie.Ratings.ToEntity();
        movie.TmdbId = radarrMovie.TmdbId;
        movie.Studio = radarrMovie.Studio;
        movie.InCinemas = radarrMovie.InCinemas;
        movie.PhysicalRelease = radarrMovie.PhysicalRelease;
        movie.YoutubeTrailerId = radarrMovie.YoutubeTrailerId;
        movie.AlternativeTitles = radarrMovie.AlternativeTitles;
        
        return movie;
    }

    public static ContentRating ToEntity(this Clients.Abstractions.ContentRating contentRating) => new()
    {
        Votes = contentRating.Votes,
        Value = contentRating.Value
    };
}