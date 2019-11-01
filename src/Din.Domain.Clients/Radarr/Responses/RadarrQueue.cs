using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Interfaces;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrQueue : Queue, IQueue<RadarrMovie>
    {
        [JsonProperty("movie")] public RadarrMovie Content { get; set; }
    }
}
