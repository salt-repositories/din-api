using System;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Content.Responses;

namespace Din.Application.WebAPI.Movies.Responses
{
    public class MovieSearchResponse : SearchResponse
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
