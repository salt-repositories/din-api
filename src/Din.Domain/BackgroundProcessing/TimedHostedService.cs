using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing;

public class TimedHostedService<T> : IHostedService, IDisposable where T : class, IBackgroundTask
{
    private readonly Container _container;
    private readonly Settings _settings;
    private readonly ILogger _logger;
    private readonly Timer _timer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public TimedHostedService(
        Container container,
        Settings settings,
        ILogger<TimedHostedService<T>> logger)
    {
        _container = container;
        _settings = settings;
        _logger = logger;
        _timer = new Timer(callback: _ => DoWork());
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Change(dueTime: TimeSpan.Zero, period: _settings.Interval);
        return Task.CompletedTask;
    }

    private void DoWork()
    {
        try
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _container.GetInstance<T>().ExecuteAsync(_cancellationTokenSource.Token).Wait();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _timer.Dispose();
    }

    public class Settings
    {
        public readonly TimeSpan Interval;

        public Settings(TimeSpan interval)
        {
            Interval = interval;
        }
    }
}