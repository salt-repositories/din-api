using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IGiphyClient
    {
        Task<GiphyItem> GetRandomGifAsync(string query);
    }
}
