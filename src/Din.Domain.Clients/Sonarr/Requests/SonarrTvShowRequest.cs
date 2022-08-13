using System.Collections.Generic;
using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Requests
{
    public class SonarrTvShowRequest : ContentRequest
    {
        [JsonProperty("tvdbId")] public string TvdbId { get; set; }
        [JsonProperty("seasons")] public ICollection<SonarrRequestSeason> Seasons { get; set; }
        [JsonProperty("seasonFolder")] public bool SeasonFolder { get; set; }
        [JsonProperty("addOptions")] public SonarrRequestAddOptions AddOptions { get; set; }
        [JsonProperty("languageProfileId")] public int LanguageProfileId { get; set; }
    }

    public class SonarrRequestSeason
    {
        [JsonProperty("seasonNumber")] public int SeasonNumber { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
    
    public class SonarrRequestAddOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")] public bool IgnoreEpisodesWithFiles { get; set; }
        [JsonProperty("ignoreEpisodesWithoutFiles")] public bool IgnoreEpisodesWithoutFiles { get; set; }
        [JsonProperty("searchForMissingEpisodes")] public bool SearchForMissingEpisodes { get; set; }
    }
}
