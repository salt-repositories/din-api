﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

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

        private async void ExecuteTasks(object state)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                try
                {
                    var tasks = _container.GetAllInstances<IBackgroundTask>()
                        .Select(task => task.ExecuteAsync(_cancellationToken)).ToList();

                    await Task.WhenAll(tasks);
                }
                catch (Exception exception)
                {
                    _container.GetInstance<ILogger<BackgroundTaskProcessor>>().LogError(exception, "Uncaught exception within background task");
                }
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