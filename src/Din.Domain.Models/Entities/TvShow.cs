using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public class TvShow : IContent
    {
        public Guid Id { get; set; }
        public int SystemId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public ICollection<TvShowGenre> Genres { get; set; }
        public string Status { get; set; }
        public bool Downloaded { get; set; }
        public bool HasFile { get; set; }
        public string Year { get; set; }
        public DateTime Added { get; set; }
        public string PlexUrl { get; set; }
        public string PosterPath { get; set; }
        public ContentRating Ratings { get; set; }
        public ICollection<string> AlternativeTitles { get; set; }
        public int TvdbId { get; set; }
        public int SeasonCount { get; set; }
        public int TotalEpisodeCount { get; set; }
        public int EpisodeCount { get; set; }
        public string Network { get; set; }
        public string AirTime { get; set; }
        public DateTime FirstAired { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}