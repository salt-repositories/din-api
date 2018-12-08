using System.Collections.Generic;

namespace Din.Service.Dtos
{
    public class TvShowDto
    {
        public int SystemId { get; set; }
        public int TvdbId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public ICollection<SeasonDto> Seasons { get; set; }
    }

    public class SeasonDto
    {
        public int SeasonsNumber { get; set; }
        public SeasonStatistics Statistics { get; set; }
    }

    public class SeasonStatistics
    {
        public string EpisodeCount { get; set; }
        public string TotalEpisodeCount { get; set; }
    }
}
