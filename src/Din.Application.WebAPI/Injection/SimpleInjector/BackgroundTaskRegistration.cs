using System.Reflection;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class BackgroundTaskRegistration
    {
        public static void RegisterBackgroundTasks(this Container container, Assembly[] assemblies)
        {
            container.Collection.Register<IBackgroundTask>(assemblies);
            container.Register<ContentPollingQueue>(Lifestyle.Singleton);
        }
    }
}
