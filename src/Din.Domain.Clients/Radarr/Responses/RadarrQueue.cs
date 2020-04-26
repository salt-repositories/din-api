using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrQueue : Queue
    {
        [JsonProperty("movie")] public RadarrMovie Movie { get; set; }
    }
}
