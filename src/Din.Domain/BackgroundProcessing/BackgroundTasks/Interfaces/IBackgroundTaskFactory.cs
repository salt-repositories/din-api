namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;

public interface IBackgroundTaskFactory
{
    IBackgroundTask Create(string taskName);
}