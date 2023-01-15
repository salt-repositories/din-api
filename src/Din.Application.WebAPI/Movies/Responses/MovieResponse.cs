using System;
using System.Linq;
using Din.Application.WebAPI.Content.Responses;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Movies.Responses
{
    public record MovieResponse : ContentResponse
    {
        public int TmdbId { get; set; }
        public string Studio { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime PhysicalRelease { get; set; }
        public string YoutubeTrailerId { get; set; }

        public static implicit operator MovieResponse(Movie movie) => new()
        {
            Id = movie.Id,
            SystemId = movie.SystemId,
            ImdbId = movie.ImdbId,
            Title = movie.Title,
            Overview = movie.Overview,
            Genres = movie.Genres.Select(genre => genre.Genre.Name),
            Status = movie.Status,
            Downloaded = movie.Downloaded,
            HasFile = movie.HasFile,
            Year = movie.Year,
            Added = movie.Added,
            PlexUrl = movie.PlexUrl,
            PosterPath = movie.PosterPath,
            Ratings = movie.Ratings,
            TmdbId = movie.TmdbId,
            Studio = movie.Studio,
            InCinemas = movie.InCinemas,
            PhysicalRelease = movie.PhysicalRelease,
            YoutubeTrailerId = movie.YoutubeTrailerId
        };

        public static implicit operator MovieResponse(RadarrMovie radarrMovie) => new()
        {
            SystemId = radarrMovie.SystemId,
            ImdbId = radarrMovie.ImdbId,
            Title = radarrMovie.Title,
            Overview = radarrMovie.Overview,
            Genres = radarrMovie.Genres,
            Status = radarrMovie.Status,
            Downloaded = radarrMovie.Downloaded,
            HasFile = radarrMovie.HasFile,
            Year = radarrMovie.Year,
            Added = radarrMovie.Added,
            Ratings = radarrMovie.Ratings,
            TmdbId = radarrMovie.TmdbId,
            Studio = radarrMovie.Studio,
            InCinemas = radarrMovie.InCinemas,
            PhysicalRelease = radarrMovie.PhysicalRelease,
            YoutubeTrailerId = radarrMovie.YoutubeTrailerId
        };
    }
}