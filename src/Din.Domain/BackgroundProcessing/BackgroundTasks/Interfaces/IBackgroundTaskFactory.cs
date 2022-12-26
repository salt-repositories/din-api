namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;

public interface IBackgroundTaskFactory
{
    IBackgroundTask Create<T>() where T : class, IBackgroundTask;
}