using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundTasks.Concrete
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
            _timer = new Timer(ExecuteTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private void ExecuteTasks(object state)
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var tasks = _container.GetAllInstances<IBackgroundTask>().Select(task => task.Execute(_cancellationToken)).ToList();

                Task.WhenAll(tasks);
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
