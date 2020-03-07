using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Repositories.Abstractions
{
    public class BaseRepository : IBaseRepository
    { 
        protected readonly DinContext Context;

        protected BaseRepository(DinContext context)
        {
            Context = context;
        }

        public Task<int> Count<T>(CancellationToken cancellationToken) where T : class
        {
            IQueryable<T> query = Context.Set<T>();
 
            return query.CountAsync(cancellationToken);
        }

        public void Insert<T>(T entity) where T : class
        {
            Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }
    }
}
