using System.Threading.Tasks;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.ResponseObjects;

namespace Din.Domain.Clients.Interfaces
{
    public interface IMovieClient : IContentClient
    {
        Task<T> GetCurrentMoviesAsync<T>(int pageSize, int page, string sortKey, string sortDirection);
        Task<McMovie> GetMovieByIdAsync(int id);
        Task<(bool status, int systemId)> AddMovieAsync(McRequest movie);
    }
}
