using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces
{
    public interface IBackgroundTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
