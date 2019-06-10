using System;

namespace Din.Application.WebAPI.Models.Response
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