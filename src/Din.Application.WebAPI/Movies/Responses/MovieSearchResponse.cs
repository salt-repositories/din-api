using System;
using Din.Application.WebAPI.Content;

namespace Din.Application.WebAPI.Movies.Responses
{
    public class MovieSearchResponse : SearchResponse
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
