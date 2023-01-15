using System;
using System.Linq;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Content.Responses;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Responses
{
    public record MovieSearchResponse : SearchResponse
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public DateTime ReleaseDate { get; set; }

        public static implicit operator MovieSearchResponse(SearchMovie searchMovie)
        {
            var response =  new MovieSearchResponse
            {
                TmdbId = searchMovie.Id,
                Overview = searchMovie.Overview,
                PosterPath = searchMovie.PosterPath,
                Genres = searchMovie.GenreIds.Select(id => ((TmdbGenre) id).ToString()),
                OriginalLanguage = searchMovie.OriginalLanguage,
                VoteAverage = searchMovie.VoteAverage,
                VoteCount = searchMovie.VoteCount,
                Title = searchMovie.Title,
                OriginalTitle = searchMovie.OriginalTitle,
            };

            if (searchMovie.ReleaseDate.HasValue)
            {
                response.ReleaseDate = searchMovie.ReleaseDate.Value;
            }

            return response;
        }
    }
}