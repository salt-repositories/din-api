using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Stores.Interfaces;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing
{
    public class BackgroundTaskProcessor : IHostedService, IDisposable
    {
        private readonly Container _container;
        private CancellationToken _cancellationToken;
        private Timer _timer;

        public BackgroundTaskProcessor(Container container)
        {
            _container = container;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _timer = new Timer(ExecuteTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private void ExecuteTasks(object state)
        {
            var taskStore = _container.GetInstance<ITaskStore>();
            
            foreach (var task in _container.GetAllInstances<IBackgroundTask>())
            {
                taskStore.AddBackgroundTask(task);
                task.ExecuteAsync(_cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}