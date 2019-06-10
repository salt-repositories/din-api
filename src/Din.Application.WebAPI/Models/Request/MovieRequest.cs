namespace Din.Application.WebAPI.Models.Request
{
    public class MovieRequest
    {
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string PosterPath { get; set; }
    }
}
