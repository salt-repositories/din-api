using System;
using System.Collections.Generic;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;

public class BackgroundTaskFactory : IBackgroundTaskFactory
{
    private readonly Container _container;

    public BackgroundTaskFactory(Container container)
    {
        _container = container;
    }
    
    public IBackgroundTask Create<T>() where T : class, IBackgroundTask
    {
        return _container.GetInstance<T>();
    }
}