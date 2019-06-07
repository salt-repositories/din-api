using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Abstractions
{
    public class BaseRepository : IBaseRepository
    { 
        protected readonly DinContext _context;

        protected BaseRepository(DinContext context)
        {
            _context = context;
        }

        public async Task<int> Count<T>(CancellationToken cancellationToken) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
 
            return await query.CountAsync(cancellationToken);
        }

        public void Insert<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
    }
}
