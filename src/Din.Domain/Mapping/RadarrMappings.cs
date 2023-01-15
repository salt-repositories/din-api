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
        YoutubeTrailerId = radarrMovie.YoutubeTrailerId
    };

    public static ContentRating ToEntity(this Clients.Abstractions.ContentRating contentRating) => new()
    {
        Votes = contentRating.Votes,
        Value = contentRating.Value
    };
}