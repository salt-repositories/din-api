using System.Collections.Generic;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Requests;

namespace Din.Application.WebAPI.Movies.Requests
{
    public record struct MovieRequest
    {
        public int TmdbId { get; init; }
        public string Title { get; init; }
        public int Year { get; init; }
        public string PosterPath { get; init; }

        public static implicit operator RadarrMovieRequest(MovieRequest request) => new()
        {
            TmdbId = request.TmdbId,
            Title = request.Title,
            Year = request.Year,
            TitleSlug = $"{request.Title.ToLower().Replace(" ", "-")}-{request.Year}",
            QualityProfileId = 6,
            Monitored = true,
            Images = new List<ContentImage>
            {
                new()
                {
                    CoverType = "poster",
                    Url = request.PosterPath
                }
            },
            MovieOptions = new RadarrRequestOptions
            {
                SearchForMovie = true
            }
        };
    }
}
