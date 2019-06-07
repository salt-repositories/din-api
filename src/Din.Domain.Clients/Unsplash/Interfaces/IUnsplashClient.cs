using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.Unsplash.Response;

namespace Din.Domain.Clients.Unsplash.Interfaces
{
    public interface IUnsplashClient
    {
        Task<IEnumerable<UnsplashImage>> GetImagesAsync();
    }
}
