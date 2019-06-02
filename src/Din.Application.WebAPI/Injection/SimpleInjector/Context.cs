using Din.Application.WebAPI.Context;
using Din.Domain.Context;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class Context
    {
        public static void RegisterContexts(this Container container)
        {
            container.Register<IRequestContext, RequestContext>();
        }
    }
}
