using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Generators.Interfaces
{
    public interface IMediaGenerator
    {
        Task<IEnumerable<UnsplashItem>> GenerateBackgroundImages();
        Task<GiphyItem> GenerateGif(GiphyTag tag);
    }
}
