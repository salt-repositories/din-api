using System.Collections.Generic;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrTvShow : Content
    {
        [JsonProperty("tvdbId")] public int TvdbId { get; set; }
        [JsonProperty("seasonCount")] public int SeasonCount { get; set; }
        [JsonProperty("totalEpisodeCount")] public int TotalEpisodeCount { get; set; }
        [JsonProperty("episodeCount")] public int EpisodeCount { get; set; }
        [JsonProperty("network")] public string Network { get; set; }
        [JsonProperty("airTime")] public string AirTime { get; set; }
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
