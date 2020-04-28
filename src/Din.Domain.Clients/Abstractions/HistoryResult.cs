using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public class HistoryResult<T> where T : HistoryRecord
    {
        [JsonProperty("page")] public long Page { get; set; }
        [JsonProperty("pageSize")] public long PageSize { get; set; }
        [JsonProperty("sortKey")] public string SortKey { get; set; }
        [JsonProperty("sortDirection")] public string SortDirection { get; set; }
        [JsonProperty("totalRecords")] public long TotalRecords { get; set; }
        [JsonProperty("records")] public ICollection<T> Records { get; set; }
    }

    public abstract class HistoryRecord
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("sourceTitle")] public string SourceTitle { get; set; }
        [JsonProperty("quality")] public RecordQuality Quality { get; set; }
        [JsonProperty("qualityCutoffNotMet")] public bool QualityCutoffNotMet { get; set; }
        [JsonProperty("date")] public DateTimeOffset Date { get; set; }
        [JsonProperty("downloadId")] public string DownloadId { get; set; }
        [JsonProperty("eventType")] public string EventType { get; set; }
    }

    public class RecordQuality
    {
        [JsonProperty("quality")] public Quality Quality { get; set; }
        [JsonProperty("customFormats")] public object[] CustomFormats { get; set; }
    }

    public class Quality
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("source")] public string Source { get; set; }
        [JsonProperty("resolution")] public string Resolution { get; set; }
        [JsonProperty("modifier")] public string Modifier { get; set; }
    }
}