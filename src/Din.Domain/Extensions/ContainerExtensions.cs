using System;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.Extensions;

public static class ContainerExtensions
{
    public static async Task WithRepository<T>(this Container container, Func<T, Task> work) where T : class, IBaseRepository
    {
        await using (AsyncScopedLifestyle.BeginScope(container))
        {
            await work(container.GetInstance<T>());
        }
    }
}