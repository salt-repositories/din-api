using System;
using System.Collections.Generic;
using System.Linq;
using Din.Application.WebAPI.Content.Responses;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public record TvShowSearchResponse : SearchResponse
    {
        public int TvdbId { get; init; }
        public string Name { get; init; }
        public string OriginalName { get; init; }
        public DateTime FirstAirDate { get; set; } 
        public IEnumerable<TvShowSearchSeason> Seasons { get; init; }

        public static implicit operator TvShowSearchResponse(TvShow tvShow)
        {
            
            var response = new TvShowSearchResponse
            {
                TvdbId = tvShow.Id,
                Overview = tvShow.Overview,
                PosterPath = tvShow.PosterPath,
                Genres = tvShow.Genres.Select(x => x.Name),
                VoteAverage = tvShow.VoteAverage,
                VoteCount = tvShow.VoteCount,
                Name = tvShow.Name,
                OriginalName = tvShow.OriginalName,
                Seasons = tvShow.Seasons.Select(x => (TvShowSearchSeason) x)
            };

            if (tvShow.FirstAirDate.HasValue)
            {
                response.FirstAirDate = tvShow.FirstAirDate.Value;
            }

            return response;
        }
    }

    public record struct TvShowSearchSeason
    {
        public int SeasonNumber { get; init; }
        public int EpisodeCount { get; init; }
        public string PosterPath { get; init; }

        public static implicit operator TvShowSearchSeason(SearchTvSeason season) => new()
        {
            SeasonNumber = season.SeasonNumber,
            EpisodeCount = season.EpisodeCount,
            PosterPath = season.PosterPath
        };
    }
}
