using System.Collections.Generic;

namespace Din.Application.WebAPI.Content.Responses
{
    public record SearchResponse
    {
        public int TmdbId { get; init; }
        public string Overview { get; init; }
        public string PosterPath { get; init; }
        public IEnumerable<string> Genres { get; init; }
        public string OriginalLanguage { get; init; }
        public double VoteAverage { get; init; }
        public int VoteCount { get; init; }
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