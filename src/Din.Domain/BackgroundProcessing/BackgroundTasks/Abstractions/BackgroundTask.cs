using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions
{
    public abstract class BackgroundTask : IBackgroundTask
    {
        private readonly Stopwatch _stopwatch;
        private readonly Container _container;
        
        protected readonly ILogger<BackgroundTask> Logger;

        public event Action<string> BackgroundTaskTriggered;
        public event Action<IBackgroundTask> ExecutionCompleted;
        
        public string Name { get; }
        
        private double _progress;
        public double Progress => Math.Round(_progress / AmountOfWork * 100, 0);
        
        public TimeSpan ExecutionTime => _stopwatch.Elapsed;
        
        protected virtual IEnumerable<string> Triggers => Array.Empty<string>();
        protected double AmountOfWork { get; set; }

        protected BackgroundTask(Container container, ILogger<BackgroundTask> logger, string name)
        {
            _stopwatch = new Stopwatch();
            _container = container;
           
            Logger = logger;

            Name = name;
            _progress = 0;
            AmountOfWork = 0;
        }

        protected abstract Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken);

        public Task ExecuteAsync(CancellationToken cancellationToken) => Task.Run(async () =>
        {
            _stopwatch.Start();
            
            await using var scope = AsyncScopedLifestyle.BeginScope(_container);

            try
            {
                await OnExecuteAsync(scope, cancellationToken);
                Logger.LogInformation($"{Name}: ExecutionTime = {ExecutionTime.Seconds}s");

                foreach (var trigger in Triggers)
                {
                    BackgroundTaskTriggered?.Invoke(trigger);
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, $"Unexpected exception in background task: {GetType().Name}");
            }

            _stopwatch.Stop();
            ExecutionCompleted?.Invoke(this);
        }, cancellationToken);

        protected void IncreaseProgress(double increment)
        {
            _progress += increment;
        }

        protected void CompleteProgress()
        {
            _progress = 100;
            AmountOfWork = 100;
        }

        protected void EnumerateAndDoWork<T>(IEnumerable<T> enumerable, Action<T> work)
        {
            var items = enumerable.ToList();
            
            AmountOfWork = items.Count;

            foreach (var item in items)
            {
                work(item);
                IncreaseProgress(1);
            }
        }

        protected async Task EnumerateAndDoWorkAsync<T>(IEnumerable<T> enumerable, Func<T, Task> work)
        {
            var items = enumerable.ToList();
            
            AmountOfWork = items.Count;

            foreach (var item in items)
            {
                await work(item);
                IncreaseProgress(1);
            }
        }
    }
}