using System.Threading.Tasks;
using Din.Domain.Clients.Giphy.Response;

namespace Din.Domain.Clients.Giphy.Interfaces
{
    public interface IGiphyClient
    {
        Task<GiphyResponse> GetRandomGifAsync(string query);
    }
}
