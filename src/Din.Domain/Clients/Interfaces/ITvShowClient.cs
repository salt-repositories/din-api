using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Clients.Interfaces
{
    public interface ITvShowClient : IContentClient
    {
        Task<IEnumerable<TcTvShow>> GetCurrentTvShowsAsync();
        Task<TcTvShow> GetTvShowByIdAsync(int id);
        Task<(bool status, int systemId)> AddTvShowAsync(TcRequest tvShow);
    }
}
