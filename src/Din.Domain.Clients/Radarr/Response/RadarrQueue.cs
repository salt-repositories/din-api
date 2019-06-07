using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Response
{
    public class RadarrQueue
    {
        [JsonProperty("movie")] public RadarrMovie Movie { get; set; }
    }
}
