using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IMovieClient : IContentClient
    {
        Task<IEnumerable<McMovie>> GetCurrentMoviesAsync();
        Task<McMovie> GetMovieByIdAsync(int id);
        Task<(bool status, int systemId)> AddMovieAsync(McRequest movie);
    }
}
