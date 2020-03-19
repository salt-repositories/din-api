using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public class ContentRating
    {
        [JsonProperty("votes")] public int Votes { get; set; }
        [JsonProperty("value")] public double Value { get; set; }
    }
}
