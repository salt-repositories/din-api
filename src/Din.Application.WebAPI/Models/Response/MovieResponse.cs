using System;
using System.Collections.Generic;

namespace Din.Application.WebAPI.Models.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Status { get; set; }
        public bool Downloaded { get; set; }
        public bool HasFile { get; set; }
        public string Year { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime Added { get; set; }
        public string YoutubeTrailerId { get; set; }
    }
}