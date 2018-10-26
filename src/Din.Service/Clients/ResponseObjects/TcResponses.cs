using System;
using System.Collections.Generic;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects.Abstractions;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    #region TvShow

    public class TcTvShow
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("tvdbId")] public int TvdbId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("seasons")] public ICollection<TcSeason> Seasons { get; set; }
    }

    public class TcSeason
    {
        [JsonProperty("seasonNumber")] public int SeasonsNumber { get; set; }
        [JsonProperty("statistics")] public TcSeasonStatistics Statistics { get; set; }
    }

    public class TcSeasonStatistics
    {
        [JsonProperty("episodeCount")] public string EpisodeCount { get; set; }
        [JsonProperty("totalEpisodeCount")] public string TotalEpisodeCount { get; set; }
    }

    #endregion TvShow

    #region Calendar

    public class TcCalendar : ContentCalendar
    {
        [JsonProperty("seasonNumber")] public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")] public int EpisodeNumber { get; set; }
        [JsonProperty("airDate")] public DateTime AirDate { get; set; }
        [JsonProperty("airDateUtC")] public DateTime AirDateUtC { get; set; }
        [JsonProperty("series")] public TcRequest Series { get; set; }
    }

    #endregion Calendar

    #region QueueItem

    public class TcQueueItem : QueueItem
    {
        [JsonProperty("series")] public TcTvShow TvShow { get; set; }
    }

    #endregion QueueItem
}