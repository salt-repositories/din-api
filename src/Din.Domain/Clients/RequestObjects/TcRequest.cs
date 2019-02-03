using System.Collections.Generic;
using Din.Domain.Clients.RequestObjects.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.RequestObjects
{
    public class TcRequest : ContentRequest
    {
        [JsonProperty("tvdbid")] public string TvShowId { get; set; }
        [JsonProperty("seasons")] public ICollection<TcRequestSeason> Seasons { get; set; }
    }

    public class TcRequestSeason
    {
        [JsonProperty("seasonNumber")] public string SeasonNumber { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
}