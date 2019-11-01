using System;
using System.Collections.Generic;
using Din.Application.WebAPI.Content.Responses;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public class TvShowSearchResponse : SearchResponse
    {
        public int TvdbId { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public DateTime FirstAirDate { get; set; } 
        public IEnumerable<TvShowSearchSeason> Seasons { get; set; }
    }

    public class TvShowSearchSeason
    {
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public string PosterPath { get; set; }
    }
}
