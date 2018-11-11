using System;
using System.Collections.Generic;
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

    public class McMovieContainer
    {
        [JsonProperty("page")] public int Page { get; set; }
        [JsonProperty("pageSize")] public int PageSize { get; set; }
        [JsonProperty("sortKey")] public string SortKey { get; set; }
        [JsonProperty("sortDirection")] public string SortDirection { get; set; }
        [JsonProperty("totalRecords")] public int TotalRecords { get; set; }
        [JsonProperty("records")] public ICollection<McMovie> Records { get; set; }
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