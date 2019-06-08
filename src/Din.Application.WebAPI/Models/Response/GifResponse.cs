namespace Din.Application.WebAPI.Models.Response
{
    public class GifResponse
    { 
        public string Title { get; set; }
        public string Url { get; set; }
        public string Mp4Url { get; set; }
        public int Frames { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}