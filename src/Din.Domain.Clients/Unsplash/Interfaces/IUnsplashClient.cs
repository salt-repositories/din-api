using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Domain.Clients.Unsplash.Interfaces
{
    public interface IUnsplashClient
    {
        Task<IEnumerable<UnsplashImage>> GetImagesAsync(CancellationToken cancellationToken);
    }
}
