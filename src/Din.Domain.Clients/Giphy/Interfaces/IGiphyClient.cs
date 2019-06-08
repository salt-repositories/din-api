using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.Clients.Giphy.Interfaces
{
    public interface IGiphyClient
    {
        Task<Responses.Giphy> GetRandomGifByTagAsync(string tag, CancellationToken cancellationToken);
    }
}
