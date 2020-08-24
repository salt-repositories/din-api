using System;

namespace Din.Domain.Models.Entities
{
    public class Episode
    {
        public Guid Id { get; set; }
        public TvShow TvShow { get; set; }
        public Guid TvShowId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get;set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public bool Monitored { get; set; }
    }
}
