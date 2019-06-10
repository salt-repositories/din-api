using System;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrCalendar
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("seriesId")] public int TvShowId { get; set; }
        [JsonProperty("seasonNumber")] public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")] public int EpisodeNumber { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("overview")] public string Overview { get; set; }
        [JsonProperty("airDateUtc")] public DateTime AirDateUtc { get; set; }
        [JsonProperty("airDate")] public DateTime AirDate { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
        [JsonProperty("series")] public SonarrTvShow TvShow { get; set; }
    }
}
