using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Din.Domain.Extensions
{
    /// <summary>
    /// <see cref="AppDomain"/> extensions
    /// </summary>
    public static class AppDomainExtensions
    {
        /// <summary>
        /// Gets the application assemblies.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <returns></returns>
        public static Assembly[] GetApplicationAssemblies(this AppDomain appDomain)
        {
            var appDomainAssemblies = appDomain.GetAssemblies();

            var rootApplicationAssembly =
                appDomainAssemblies.Single(asm => asm.GetName().Name == appDomain.FriendlyName);

            var applicationNamespaceRootSegment = typeof(AppDomainExtensions).GetNamespaceRootSegment();
            return appDomainAssemblies
                .SelectMany(asm => asm.GetReferencedAssemblies())
                .Where(asm => asm.FullName.StartsWith(applicationNamespaceRootSegment))
                .Distinct(new AssemblyNameComparer())
                .Select(a => Assembly.Load(a))
                .Concat(new[] { rootApplicationAssembly })
                .ToArray();
        }

        private class AssemblyNameComparer : IEqualityComparer<AssemblyName>
        {
            public bool Equals(AssemblyName x, AssemblyName y)
            {
                return x?.FullName == y?.FullName;
            }

            public int GetHashCode(AssemblyName obj)
            {
                return obj.FullName.GetHashCode();
            }
        }
    }
}
