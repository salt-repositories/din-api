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
        private CancellationToken _cancellationToken;

        private IBackgroundTaskFactory _backgroundTaskFactory;
        private Timer _timer;

        public BackgroundTaskProcessor(Container container)
        {
            _container = container;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _backgroundTaskFactory = _container.GetInstance<IBackgroundTaskFactory>();

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
            var tasks = new[]
            {
                _backgroundTaskFactory.Create(nameof(ArchiveAuthorizationCodes)),
                _backgroundTaskFactory.Create(nameof(UpdateMovieDatabase)),
                _backgroundTaskFactory.Create(nameof(UpdateTvShowDatabase))
            };
            
            foreach (var task in tasks)
            {
                ExecuteTask(task);
            }
        }

        private void ExecuteTask(IBackgroundTask task)
        {
            task.BackgroundTaskTriggered += OnBackgroundTaskTriggered;
            task.ExecutionCompleted += OnExecutionCompleted;
            task.ExecuteAsync(_cancellationToken);
        }
        
        private void OnBackgroundTaskTriggered(string taskName)
        {
            ExecuteTask(_backgroundTaskFactory.Create(taskName));
        }

        private void OnExecutionCompleted(IBackgroundTask task)
        {
            task.BackgroundTaskTriggered -= OnBackgroundTaskTriggered;
            task.ExecutionCompleted -= OnExecutionCompleted;
        }
    }
}