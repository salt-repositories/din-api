using System;
using System.Threading;
using System.Threading.Tasks;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces
{
    public interface IBackgroundTask
    {
        string Name { get; }
        double Progress { get; }
        TimeSpan ExecutionTime { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
