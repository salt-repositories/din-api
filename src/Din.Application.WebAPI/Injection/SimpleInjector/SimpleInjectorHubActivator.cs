using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Internal;
using Microsoft.AspNetCore.SignalR.Protocol;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public class SimpleInjectorHubActivator<T> : IHubActivator<T> where T : Hub
    {
        private readonly Container _container;

        public SimpleInjectorHubActivator(Container container)
        {
            _container = container;
        }

        public T Create()
        {
            return _container.GetInstance<T>();
        }

        public void Release(T hub)
        {
        }
    }

    public sealed class SimpleInjectorScopeHubDispatcher<THub> : HubDispatcher<THub> where THub : Hub
    {
        private readonly Container _container;
        private readonly HubDispatcher<THub> _decorated;

        public SimpleInjectorScopeHubDispatcher(
            Container container, DefaultHubDispatcher<THub> decorated)
        {
            this._container = container;
            this._decorated = decorated;
        }

        public override async Task DispatchMessageAsync(HubConnectionContext connection, HubMessage m)
        {
            using (BeginScope())
            {
                await _decorated.DispatchMessageAsync(connection, m);
            }
        }

        public override async Task OnConnectedAsync(HubConnectionContext connection)
        {
            using (BeginScope())
            {
                await _decorated.OnConnectedAsync(connection);
            }
        }

        public override async Task OnDisconnectedAsync(HubConnectionContext connection, Exception ex)
        {
            using (BeginScope())
            {
                await _decorated.OnDisconnectedAsync(connection, ex);
            }
        }

        public override IReadOnlyList<Type> GetParameterTypes(string name)
        {
            return _decorated.GetParameterTypes(name);
        }

        public override Type GetReturnType(string invocId)
        {
            return _decorated.GetReturnType(invocId);
        }

        private Scope BeginScope()
        {
            return AsyncScopedLifestyle.BeginScope(_container);
        }
    }
}