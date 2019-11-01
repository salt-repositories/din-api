using System.Collections.Generic;
using Din.Application.WebAPI.Content.Responses;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public class TvShowResponse : ContentResponse
    {
        public int TvdbId { get; set; }
        public int SeasonCount { get; set; }
        public int TotalEpisodeCount { get; set; }
        public int EpisodeCount { get; set; }
        public string Network { get; set; }
        public string AirTime { get; set; }
        public ICollection<SeasonResponse> Seasons { get; set; }
    }

    public class SeasonResponse
    {
        public int SeasonsNumber { get; set; }
        public SeasonStatisticsResponse Statistics { get; set; }
    }

    public class SeasonStatisticsResponse
    {
        public string EpisodeCount { get; set; }
        public string TotalEpisodeCount { get; set; }
    }
}