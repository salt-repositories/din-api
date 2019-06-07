using Newtonsoft.Json;

namespace Din.Domain.Clients.Giphy.Response
{
    public class GiphyResponse
    {
        [JsonProperty("data")] public GiphyData Data { get; set; }
    }

    public class GiphyData
    {
        [JsonProperty("image_original_url")] public string ImageOriginalUrl { get; set; }
    }
}