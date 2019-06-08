using System;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrCalendar : Calendar
    {
        [JsonProperty("seasonNumber")] public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")] public int EpisodeNumber { get; set; }
        [JsonProperty("airDate")] public DateTime AirDate { get; set; }
        [JsonProperty("airDateUtC")] public DateTime AirDateUtC { get; set; }
        [JsonProperty("series")] public SonarrTvShow Series { get; set; }
    }
}
