using System;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrCalendar : Calendar
    {
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")] public DateTime PhysicalRelease { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
    }
}
