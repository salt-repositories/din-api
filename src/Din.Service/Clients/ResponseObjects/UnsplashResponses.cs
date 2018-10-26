using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class UnsplashItem
    {
        [JsonProperty("urls")] public UnsplashFileTypes FileTypes { get; set; }
    }

    public class UnsplashFileTypes
    {
        [JsonProperty("full")] public string Full { get; set; }
        [JsonProperty("regular")] public string Regular { get; set; }
    }
}