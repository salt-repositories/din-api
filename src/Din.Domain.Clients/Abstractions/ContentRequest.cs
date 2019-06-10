using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public abstract class ContentRequest
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("qualityProfileId")] public int QualityProfileId { get; set; }
        [JsonProperty("profileId")] public string ProfileId { get; set; }
        [JsonProperty("titleslug")] public string TitleSlug { get; set; }
        [JsonProperty("images")] public ICollection<ContentImage> Images { get; set; }
        [JsonProperty("rootFolderPath")] public string RootFolderPath { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
    
    public class ContentImage
    {
        [JsonProperty("covertype")] public string CoverType { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }
}
