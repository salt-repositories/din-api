namespace Din.Domain.Models.Dtos
{
    public class PlexPosterDto
    {
        public string Title { get; set; }
        public string PlexUrl { get; set; }
        public string PosterPath { get; set; }

        public override bool Equals(object? obj)
        {
            return obj != null && Title.Equals(((PlexPosterDto) obj).Title);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
