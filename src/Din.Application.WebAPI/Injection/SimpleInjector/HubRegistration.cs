﻿using Din.Application.WebAPI.Content.HubTasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class HubRegistration
    {
        public static void RegisterSignalRCorePipeline(this IServiceCollection services, Container container)
        {
            services.AddSingleton(container);
            services.AddSingleton(typeof(IHubActivator<>), typeof(SimpleInjectorHubActivator<>));
        }

        public static void RegisterHubTasks(this Container container)
        {
            container.Register<CurrentQueueTimedTask>(Lifestyle.Singleton);
        }
    }
}