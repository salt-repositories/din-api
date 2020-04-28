using Din.Domain.Clients.Abstractions;
using Newtonsoft.Json;

namespace Din.Domain.Clients.Radarr.Responses
{
    public class RadarrHistoryRecord : HistoryRecord
    {
        [JsonProperty("movieId")] public long MovieId { get; set; }
        [JsonProperty("movie")] public RadarrMovie Movie { get; set; }
    }
}
