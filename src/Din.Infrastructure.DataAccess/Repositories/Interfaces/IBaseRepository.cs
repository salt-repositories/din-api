using System.Threading;
using System.Threading.Tasks;

namespace Din.Infrastructure.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> Count<T>(CancellationToken cancellationToken) where T : class;
        void Insert<T>(T entity) where T : class;
    }
}
