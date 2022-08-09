using System;
using System.Collections.Generic;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;

public class BackgroundTaskFactory : Dictionary<string, Type>, IBackgroundTaskFactory
{
    private readonly Container _container;

    public BackgroundTaskFactory(Container container)
    {
        _container = container;
    }
    
    public IBackgroundTask Create(string taskName)
    {
        return (IBackgroundTask) _container.GetInstance(this[taskName]);
    }
}