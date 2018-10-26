using System;
using Din.Service.Clients.ResponseObjects.Abstractions;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    #region Movie

    public class McMovie
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
    }

    #endregion Movie

    #region Calendar

    public class McCalendar : ContentCalendar
    {
        [JsonProperty("tmdbid")] public int Id { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("inCinemas")] public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")] public DateTime PhysicalRelease { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
    }

    #endregion Calendar

    #region QueueItem

    public class McQueueItem : QueueItem
    {
        [JsonProperty("movie")] public McMovie Movie { get; set; }
    }

    #endregion QueueItem
}