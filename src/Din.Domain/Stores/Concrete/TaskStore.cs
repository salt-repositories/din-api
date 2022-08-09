using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Domain.Stores.Interfaces;
using Microsoft.Extensions.Logging;

namespace Din.Domain.Stores.Concrete;

public class TaskStore : ITaskStore, IDisposable
{
    private readonly ILogger<TaskStore> _logger;
    private readonly ConcurrentDictionary<string, IBackgroundTask> _backgroundTasks;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public TaskStore(ILogger<TaskStore> logger)
    {
        _logger = logger;
        _backgroundTasks = new ConcurrentDictionary<string, IBackgroundTask>();
        _cancellationTokenSource = new CancellationTokenSource();
        // Task.Run(Notify, _cancellationTokenSource.Token);
        Task.Run(Cleanup, _cancellationTokenSource.Token);
    }

    public IEnumerable<IBackgroundTask> GetActiveBackgroundTasks()
    {
        return _backgroundTasks.Values;
    }

    public void AddBackgroundTask(IBackgroundTask task)
    {
        _backgroundTasks.TryAdd(task.Name, task);
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
    }

    private void Notify()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            foreach (var task in _backgroundTasks.Values)
            {
                _logger.LogInformation($"{task.Name}: ExecutionTime = {task.ExecutionTime.Seconds}s Progress = {task.Progress}%");
            }
            
            Thread.Sleep(1000);
        }
    }

    private void Cleanup()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            foreach (var task in _backgroundTasks.Values.Where(x => x.Progress == 100))
            {
                _backgroundTasks.TryRemove(task.Name, out _);
            }
        }
    }
}