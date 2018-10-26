using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects.Abstractions
{
    public abstract class ContentCalendar
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("overview")] public string Overview { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
    }
}