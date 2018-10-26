using System;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects.Abstractions
{
    public abstract class QueueItem
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("size")] public double Size { get; set; }
        [JsonProperty("sizeleft")] public double SizeLeft { get; set; }
        [JsonProperty("timeleft")] public TimeSpan TimeLeft { get; set; }
        [JsonProperty("estimatedCompletionTime")] public DateTime Eta { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("downloadId")] public string DownloadId { get; set; }
        [JsonProperty("protocol")] public string Protocol { get; set; }
    }
}
