namespace Din.Application.WebAPI.Movies.Requests
{
    public class MovieRequest
    {
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string PosterPath { get; set; }
    }
}
