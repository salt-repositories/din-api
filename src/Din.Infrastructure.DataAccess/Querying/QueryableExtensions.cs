using System;
using System.Linq;
using System.Linq.Expressions;
using Din.Domain.Models.Querying;

namespace Din.Infrastructure.DataAccess.Querying
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query, QueryParameters<T> queryParameters)
        {
            if (queryParameters == null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                var param = Expression.Parameter(typeof(T), "x");
                Expression conversion = Expression.Convert(Expression.Property(param, queryParameters.SortBy), typeof(object));
                var expression = Expression.Lambda<Func<T, object>>(conversion, param);

                query = queryParameters.SortDirection == SortDirection.Asc ? query.OrderBy(expression) : query.OrderByDescending(expression);
            }

            query = query.Skip(queryParameters.Skip).Take(queryParameters.Take);

            return query;
        }
    }
}
