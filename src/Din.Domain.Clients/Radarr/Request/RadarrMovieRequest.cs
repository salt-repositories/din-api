using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Request
{
    public class RadarrMovieRequest
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("qualityProfileId")] public int QualityProfileId { get; set; }
        [JsonProperty("profileId")] public string ProfileId { get; set; }
        [JsonProperty("titleslug")] public string TitleSlug { get; set; }
        [JsonProperty("images")] public ICollection<RadarrRequestImage> Images { get; set; }
        [JsonProperty("rootFolderPath")] public string RootFolderPath { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
        [JsonProperty("year")] public int Year { get; set; }
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("addOptions")] public RadarrRequestOptions MovieOptions { get; set; }
    }

    public class RadarrRequestImage
    {
        [JsonProperty("covertype")] public string CoverType { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
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