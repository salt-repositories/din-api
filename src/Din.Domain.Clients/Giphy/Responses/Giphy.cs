using Newtonsoft.Json;

namespace Din.Domain.Clients.Giphy.Responses
{
    public class Giphy
    {
        [JsonProperty("data")] public GiphyData Data { get; set; }
    }

    public class GiphyData
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("image_original_url")] public string Url { get; set; }
        [JsonProperty("image_mp4_url")] public string Mp4Url { get; set; }
        [JsonProperty("image_frames")] public int Frames { get; set; }
        [JsonProperty("image_width")] public int Width { get; set; }
        [JsonProperty("image_height")] public int Height { get; set; }

    }
}