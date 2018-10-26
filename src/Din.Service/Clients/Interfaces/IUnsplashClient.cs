using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IUnsplashClient
    {
        Task<ICollection<UnsplashItem>> GetBackgroundCollection();
    }
}
