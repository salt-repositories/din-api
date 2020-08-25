using System;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrMovie : Content
    {
        [JsonProperty("tmdbId")] public int TmdbId { get; set; }
        [JsonProperty("studio")] public string Studio { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")] public DateTime PhysicalRelease { get; set; }
        [JsonProperty("youTubeTrailerId")] public string YoutubeTrailerId { get; set; }
    }
}
