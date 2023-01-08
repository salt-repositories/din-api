using System;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.Extensions;

public static class ContainerExtensions
{
    public static async Task WithRepository<T>(this Container container, Func<T, Task> work)
        where T : class, IBaseRepository
    {
        await using (AsyncScopedLifestyle.BeginScope(container))
        {
            await work(container.GetInstance<T>());
        }
    }

    public static async Task<TResponse> WithRepository<TRepository, TResponse>(this Container container,
        Func<TRepository, Task<TResponse>> work)
        where TRepository : class, IBaseRepository
    {
        await using (AsyncScopedLifestyle.BeginScope(container))
        {
            return await work(container.GetInstance<TRepository>());
        }
    }

    public static Task WithRepository<T>(this Scope scope, Func<T, Task> work) where T : class, IBaseRepository
        => WithRepository(scope.Container, work);

    public static Task<TResponse> WithRepository<TRepository, TResponse>(this Scope scope,
        Func<TRepository, Task<TResponse>> work)
        where TRepository : class, IBaseRepository
        => WithRepository(scope.Container, work);
}