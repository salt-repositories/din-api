using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrTvShow
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("tvdbId")] public int TvdbId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("seasons")] public ICollection<Season> Seasons { get; set; }
    }
    public class Season
    {
        [JsonProperty("seasonNumber")] public int SeasonsNumber { get; set; }
        [JsonProperty("statistics")] public SeasonStatistics Statistics { get; set; }
    }

    public class SeasonStatistics
    {
        [JsonProperty("episodeCount")] public string EpisodeCount { get; set; }
        [JsonProperty("totalEpisodeCount")] public string TotalEpisodeCount { get; set; }
    }
}
