using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces
{
    public interface IBackgroundTask
    { 
        string Name { get; }
        BackgroundTaskStatus Status { get; }
        double Progress { get; }
        TimeSpan ExecutionTime { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
