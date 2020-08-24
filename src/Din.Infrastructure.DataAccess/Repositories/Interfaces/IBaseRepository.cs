using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> Count<T>(CancellationToken cancellationToken) where T : class;
        T Insert<T>(T entity) where T : class;
        Task InsertMultipleAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class;
        T Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
