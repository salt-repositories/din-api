using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Logging;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions
{
    public abstract class BackgroundTask : IBackgroundTask
    {
        protected readonly ILogger<BackgroundTask> Logger;

        public string Name { get; }
        public double Progress { get; protected set; }
        
        protected BackgroundTask(string name, ILogger<BackgroundTask> logger)
        {
            Logger = logger;

            Name = name;
            Progress = 0;
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}