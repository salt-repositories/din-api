using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Generators.Interfaces
{
    public interface IMediaGenerator
    {
        Task<IEnumerable<UnsplashItem>> GenerateBackgroundImages();
        Task<GiphyItem> GenerateGif(string query);
    }
}
