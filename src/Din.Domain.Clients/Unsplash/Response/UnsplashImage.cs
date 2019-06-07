using Newtonsoft.Json;

namespace Din.Domain.Clients.Unsplash.Response
{
    public class UnsplashImage
    {
        [JsonProperty("urls")] public UnsplashImageUrls Urls { get; set; }
    }

    public class UnsplashImageUrls
    {
        [JsonProperty("raw")] public string Raw { get; set; }
        [JsonProperty("full")] public string Full { get; set; }
        [JsonProperty("regular")] public string Regular { get; set; }
        [JsonProperty("small")] public string Small { get; set; }
        [JsonProperty("thumb")] public string Thumb { get; set; }
    }
}
