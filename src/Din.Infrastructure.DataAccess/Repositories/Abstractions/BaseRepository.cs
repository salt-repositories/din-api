using System.Collections.Generic;
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

        public T Insert<T>(T entity) where T : class
        {
            return Context.Add(entity).Entity;
        }

        public Task InsertMultipleAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class
        {
            return Context.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public T Update<T>(T entity) where T : class
        {
            return Context.Update(entity).Entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
