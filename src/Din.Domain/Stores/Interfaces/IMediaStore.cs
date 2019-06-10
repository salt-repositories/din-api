using System.Collections.Generic;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Domain.Stores.Interfaces
{
    public interface IMediaStore
    {
        IEnumerable<UnsplashImage> GetBackgrounds();
        void SetBackgrounds(IEnumerable<UnsplashImage> backgrounds);
    }
}
