using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Requests
{
    public class RadarrMovieRequest : ContentRequest
    {
        [JsonProperty("year")] public int Year { get; set; }
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("addOptions")] public RadarrRequestOptions MovieOptions { get; set; }
    }

    public class RadarrRequestOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")]
        public bool IgnoreEpisodesWithFiles { get; set; }
        [JsonProperty("ignoreEpisodesWithoutFiles")]
        public bool IgnoreEpisodesWithoutFiles { get; set; }
        [JsonProperty("searchForMovie")] public bool SearchForMovie { get; set; }
    }
}