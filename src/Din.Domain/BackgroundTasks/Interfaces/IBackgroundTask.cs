using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.BackgroundTasks.Interfaces
{
    public interface IBackgroundTask
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
