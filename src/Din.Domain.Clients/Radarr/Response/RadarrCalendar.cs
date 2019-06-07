using System;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Response
{
    public class RadarrCalendar
    {
        [JsonProperty("tmdbid")] public int Id { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")] public DateTime PhysicalRelease { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
    }
}
