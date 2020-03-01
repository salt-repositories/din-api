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
        [JsonProperty("Metadata")]
        public MetaData[] Metadata { get; set; }
    }

    public class MetaData
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
