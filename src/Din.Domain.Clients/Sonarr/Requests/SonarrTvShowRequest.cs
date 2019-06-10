using System.Collections.Generic;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Requests
{
    public class SonarrTvShowRequest : ContentRequest
    {
        [JsonProperty("tvdbid")] public string TvdbId { get; set; }
        [JsonProperty("seasons")] public ICollection<SonarrRequestSeason> Seasons { get; set; }
    }

    public class SonarrRequestSeason
    {
        [JsonProperty("seasonNumber")] public string SeasonNumber { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
}
