using System;
using Din.Domain.BackgroundProcessing;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class BackgroundTaskRegistration
    {
        public static void RegisterBackgroundTasks(this Container container)
        {
            container.Register<ContentPollingQueue>(Lifestyle.Singleton);
            container.Register<ArchiveAuthorizationCodes>(Lifestyle.Transient);
            container.Register<UpdateMovieDatabase>(Lifestyle.Transient);
            container.Register<UpdateTvShowDatabase>(Lifestyle.Transient);
            container.Register<UpdateContentPlexUrl>(Lifestyle.Transient);
            container.Register<UpdateContentPosterUrl>(Lifestyle.Transient);
            
            container.RegisterInstance(new TimedHostedService<ArchiveAuthorizationCodes>.Settings(
                interval: TimeSpan.FromDays(1)
            ));
            
            container.RegisterInstance(new TimedHostedService<UpdateMovieDatabase>.Settings(
                interval: TimeSpan.FromMinutes(30)
            ));
            
            container.RegisterInstance(new TimedHostedService<UpdateTvShowDatabase>.Settings(
                interval: TimeSpan.FromMinutes(30)
            ));
            
            container.RegisterInstance(new TimedHostedService<UpdateContentPlexUrl>.Settings(
                interval: TimeSpan.FromMinutes(30)
            ));
            
            container.RegisterInstance(new TimedHostedService<UpdateContentPosterUrl>.Settings(
                interval: TimeSpan.FromMinutes(30)
            ));
        }
    }
}
