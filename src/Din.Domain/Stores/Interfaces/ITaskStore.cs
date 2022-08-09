using System.Collections.Generic;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;

namespace Din.Domain.Stores.Interfaces;

public interface ITaskStore
{
    IEnumerable<IBackgroundTask> GetActiveBackgroundTasks();
    void AddBackgroundTask(IBackgroundTask task);
}