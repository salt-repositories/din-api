using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IGenreRepository : IBaseRepository
    {
        Genre GetGenreByName(string name);
        Task<Genre> GetGenreByNameAsync(string name, CancellationToken cancellationToken);
    }
}
