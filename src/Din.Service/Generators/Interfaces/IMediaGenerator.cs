using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Generators.Interfaces
{
    public interface IMediaGenerator
    {
        Task<IEnumerable<UnsplashItem>> GenerateBackgroundImages();
        Task<GiphyItem> GenerateGif(string query);
    }
}
