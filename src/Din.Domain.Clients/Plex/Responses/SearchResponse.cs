using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Plex.Responses
{
    public class SearchResponse
    {
        [JsonProperty("MediaContainer")]
        public MediaContainer MediaContainer { get; set; }
    }

    public class MediaContainer
    {
        [JsonProperty("hub")]
        public IEnumerable<Hub> Hub { get; set; }
    }

    public class Hub
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("hubIdentifier")]
        public string HubIdentifier { get; set; }

        [JsonProperty("Metadata")] public IEnumerable<MetaData> Metadata { get; set; } = new List<MetaData>();
    }

    public class MetaData
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
