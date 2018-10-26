using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Dto
{
    public class StatusCodeDto
    {
        public GiphyItem Gif { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
