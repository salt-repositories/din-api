using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Din.Infrastructure.DataAccess.Connections.Interfaces
{
    public interface IHealthCheckConnection
    {
        Task<bool> CheckContextHealth(CancellationToken cancellationToken);
    }
}
