using System.Threading.Tasks;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Clients.Interfaces
{
    public interface IGiphyClient
    {
        Task<GiphyItem> GetRandomGifAsync(string query);
    }
}
