using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Clients.Interfaces
{
    public interface IUnsplashClient
    {
        Task<ICollection<UnsplashItem>> GetBackgroundCollection();
    }
}
