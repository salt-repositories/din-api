using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrMovie
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("imdbid")] public string ImdbId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("genres")] public IEnumerable<string> Genres { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
        [JsonProperty("year")] public string Year { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
        [JsonProperty("added")] public DateTime Added { get; set; }
        [JsonProperty("youTubeTrailerId")] public string YoutubeTrailerId { get; set; }
    }
}
