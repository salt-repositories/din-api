using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Plex.Responses;

namespace Din.Domain.Clients.Plex.Interfaces
{
    public interface IPlexClient
    {
        Task<SearchResponse> SearchByTitle(string title, CancellationToken cancellationToken);
    }
}
