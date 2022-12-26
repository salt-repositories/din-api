using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing
{
    public class BackgroundTaskProcessor : IHostedService, IDisposable
    {
        private readonly Container _container;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private CancellationToken _cancellationToken;
        private Timer _timer;

        public BackgroundTaskProcessor(Container container, IHostApplicationLifetime applicationLifetime)
        {
            _container = container;
            _applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _timer = new Timer(ExecuteRecurringTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
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

        private void ExecuteRecurringTasks(object state)
        {
            _applicationLifetime.ApplicationStarted.WaitHandle.WaitOne(-1);
            var backgroundTaskFactory = _container.GetInstance<IBackgroundTaskFactory>();
            
            var tasks = new[]
            {
                backgroundTaskFactory.Create<ArchiveAuthorizationCodes>(),
                backgroundTaskFactory.Create<UpdateMovieDatabase>(),
                backgroundTaskFactory.Create<UpdateTvShowDatabase>()
            };

            foreach (var task in tasks)
            {
                task.ExecuteAsync(_cancellationToken);
            }
        }
    }
}