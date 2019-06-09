using Newtonsoft.Json;

namespace Din.Domain.Clients.Abstractions
{
    public abstract class Calendar
    {
        [JsonProperty("id")] public int SystemId { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("overview")] public string Overview { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
    }
}
