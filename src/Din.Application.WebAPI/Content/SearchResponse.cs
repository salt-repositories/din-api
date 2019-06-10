using System.Collections.Generic;

namespace Din.Application.WebAPI.Content
{
    public class SearchResponse
    {
        public int TmdbId { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string OriginalLanguage { get; set; }
        public int VoteAverage { get; set; }
        public int VoteCount { get; set; }
    }

    public enum TmdbGenre
    {
        Action = 28,
        Adventure = 12,
        Animation = 16,
        Comedy = 35,
        Crime = 80,
        Documentary = 99,
        Drama = 18,
        Family = 10751,
        Fantasy = 14,
        History = 36,
        Horror = 27,
        Music = 10402,
        Mystery = 9648,
        Romance = 10749,
        ScienceFiction = 878,
        TvMovie = 10770,
        Thriller = 53,
        War = 10752,
        Western = 37
    }
}