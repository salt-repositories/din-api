using System.Reflection;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class BackgroundTaskRegistration
    {
        public static void RegisterBackgroundTasks(this Container container)
        {
            container.Register<ArchiveAuthorizationCodes>(Lifestyle.Transient);
            container.Register<UpdateMovieDatabase>(Lifestyle.Transient);
            container.Register<UpdateTvShowDatabase>(Lifestyle.Transient);
            
            container.RegisterInstance<IBackgroundTaskFactory>(new BackgroundTaskFactory(container));
            
            container.Register<ContentPollingQueue>(Lifestyle.Singleton);
        }
    }
}
