using System;
using Din.Application.WebAPI.Content.Responses;

namespace Din.Application.WebAPI.Movies.Responses
{
    public class MovieResponse : ContentResponse
    {
        public int TmdbId { get; set; }
        public string Studio { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime PhysicalRelease { get; set; }
        public string YoutubeTrailerId { get; set; }
    }
}