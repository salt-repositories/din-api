using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrQueue : Queue
    {
        [JsonProperty("series")] public SonarrTvShow TvShow { get; set; }
    }
}
