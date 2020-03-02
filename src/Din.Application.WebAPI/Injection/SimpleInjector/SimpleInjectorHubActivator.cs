using Microsoft.AspNetCore.SignalR;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public class SimpleInjectorHubActivator<T> : IHubActivator<T> where T : Hub
    {
        private readonly Container _container;

        public SimpleInjectorHubActivator(Container container)
        {
            _container = container;
        }

        public T Create() => _container.GetInstance<T>();

        public void Release(T hub)
        {
        }
    }
}