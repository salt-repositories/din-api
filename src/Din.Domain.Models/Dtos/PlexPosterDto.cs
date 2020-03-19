namespace Din.Domain.Models.Dtos
{
    public class PlexPosterDto
    {
        public string Title { get; }
        public string PlexUrl { get; }
        public string PosterPath { get; }

        public PlexPosterDto(string title, string plexUrl, string posterPath)
        {
            Title = title;
            PlexUrl = plexUrl;
            PosterPath = posterPath;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Title.Equals(((PlexPosterDto) obj).Title);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}