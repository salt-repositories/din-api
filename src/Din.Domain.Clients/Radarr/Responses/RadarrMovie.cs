using System;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrMovie : Content
    {
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("imdbid")] public string ImdbId { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
    }
}
