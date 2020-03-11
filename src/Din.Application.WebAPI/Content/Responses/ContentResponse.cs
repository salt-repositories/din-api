using System;
using System.Collections.Generic;

namespace Din.Application.WebAPI.Content.Responses
{
    public class ContentResponse
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Status { get; set; }
        public bool Downloaded { get; set; }
        public bool HasFile { get; set; }
        public string Year { get; set; }
        public DateTime Added { get; set; }
        public string PlexUrl { get; set; }
        public string PosterPath { get; set; }
    }
}