using System;

namespace Din.Domain.Models.Entities
{
    public class Movie : IContent
    {
        public Guid Id { get; set; }
        public int SystemId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Status { get; set; }
        public bool Downloaded { get; set; }
        public bool HasFile { get; set; }
        public string Year { get; set; }
        public DateTime Added { get; set; }
        public string PlexUrl { get; set; }
        public string PosterPath { get; set; }
        public ContentRating Ratings { get; set; }
        public int TmdbId { get; set; }
        public string Studio { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime PhysicalRelease { get; set; }
        public string YoutubeTrailerId { get; set; }
    }
}
