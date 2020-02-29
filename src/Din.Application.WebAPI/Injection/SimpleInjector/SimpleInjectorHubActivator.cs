using Microsoft.AspNetCore.SignalR;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public class SimpleInjectorHubActivator<T> : IHubActivator<T> where T : Hub
    {
        private readonly Container _container;
        private Scope scope;

        public SimpleInjectorHubActivator(Container container)
        {
            _container = container;
        }

        public T Create()
        {
            scope = AsyncScopedLifestyle.BeginScope(_container);
            return _container.GetInstance<T>();
        }

        public void Release(T hub)
        {
            scope.Dispose();
        }
    }
}