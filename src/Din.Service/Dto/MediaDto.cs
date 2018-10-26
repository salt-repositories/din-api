using System.Collections.Generic;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Dto
{
    public class MediaDto
    {
        public ICollection<UnsplashItem> BackgroundImageCollection { get; set; }
        public GiphyItem Gif { get; set; }
    }
}
