using System;
using System.Collections.Generic;
using System.Text;

namespace Din.Domain.Extensions
{
    /// <summary>
    /// <see cref="Type"/> extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetAssemblyName(this Type type)
        {
            return type.Assembly.GetName().Name;
        }

        /// <summary>
        /// Gets the namespace root segment.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetNamespaceRootSegment(this Type type)
        {
            return type.Namespace.Split('.')[0];
        }
    }
}
