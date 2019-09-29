using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Din.Domain.Models.Querying;

namespace Din.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ApplyQueryParameters<T>(this IEnumerable<T> collection,
            QueryParameters<T> queryParameters)
        {
            if (queryParameters == null)
            {
                return collection;
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                var param = Expression.Parameter(typeof(T), "x");
                Expression conversion = Expression.Convert(Expression.Property(param, queryParameters.SortBy), typeof(object));
                var expression = Expression.Lambda<Func<T, object>>(conversion, param);

                collection = queryParameters.SortDirection == SortDirection.Asc
                    ? collection.OrderBy(expression.Compile())
                    : collection.OrderByDescending(expression.Compile());
            }

            collection = collection.Skip(queryParameters.Skip).Take(queryParameters.Take);

            return collection;
        }
    }
}