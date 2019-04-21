using System;
using System.Linq;
using System.Reflection;

namespace Din.Domain.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets all implementation types from assembly collection using open generic interface type.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="openGenericInterfaceType">Type of the open generic.</param>
        /// <returns></returns>
        public static Type[] GetGenericInterfaceImplementationTypes(this Assembly[] assemblies, Type openGenericInterfaceType)
        {
            return assemblies.SelectMany(asm => GetGenericInterfaceImplementationTypes(asm, openGenericInterfaceType)).ToArray();
        }

        /// <summary>
        /// Gets all implementation types from assembly using open generic interface type.
        /// </summary>
        /// <param name="assembly">The assemblies.</param>
        /// <param name="openGenericInterfaceType">Type of the open generic.</param>
        /// <returns></returns>
        public static Type[] GetGenericInterfaceImplementationTypes(this Assembly assembly, Type openGenericInterfaceType)
        {
            return assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract
                    && !t.IsInterface
                    && t.GetInterfaces()
                        .Any(i =>
                            i.GetTypeInfo().IsGenericType
                            && i.GetGenericTypeDefinition() == openGenericInterfaceType
                        )
                )
                .ToArray();
        }
    }
}
