using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Concrete
{
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        public GenreRepository(DinContext context) : base(context)
        {
        }

        public Genre GetGenreByName(string name)
        {
            return Context.Genre.FirstOrDefault(g => g.Name.Equals(name));
        }

        public Task<Genre> GetGenreByNameAsync(string name, CancellationToken cancellationToken)
        {
            return Context.Genre.FirstOrDefaultAsync(g => g.Name.Equals(name), cancellationToken);
        }
    }
}