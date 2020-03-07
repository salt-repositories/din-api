using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Connections.Interfaces;

namespace Din.Infrastructure.DataAccess.Connections.Concrete
{
    public class HealthCheckConnection : DinContext, IHealthCheckConnection
    {
        public HealthCheckConnection(string connectionString) : base(connectionString)
        {
        }

        public Task<bool> CheckContextHealth(CancellationToken cancellationToken)
        {
            return Database.CanConnectAsync(cancellationToken);
        }
    }
}
