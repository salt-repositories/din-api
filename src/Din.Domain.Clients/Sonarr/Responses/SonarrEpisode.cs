using System;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrEpisode
    {
        [JsonProperty("seriesId")] public int SeriesId { get; set; }
        [JsonProperty("seasonNumber")] public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")] public int EpisodeNumber { get;set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("airDate")] public DateTime AirDate { get; set; }
        [JsonProperty("overview")] public string Overview { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
}