using System;

namespace Din.Domain.Models.Entities
{
    public class Season : IEntity
    {
        public Guid Id { get; set; }
        public Guid TvShowId { get; set; }

        public int SeasonsNumber { get; set; }
        public int EpisodeCount { get; set; }
        public int TotalEpisodeCount { get; set; }
    }
}
