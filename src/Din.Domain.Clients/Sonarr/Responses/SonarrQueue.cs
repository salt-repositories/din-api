using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Sonarr.Responses
{
    public class SonarrQueue : Queue, IQueue<SonarrTvShow>
    {
        [JsonProperty("series")] public SonarrTvShow Content { get; set; }
    }
}
