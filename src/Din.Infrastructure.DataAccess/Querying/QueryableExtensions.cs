using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Querying;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.Querying
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query,
            QueryParameters queryParameters)
        {
            if (queryParameters == null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                var param = Expression.Parameter(typeof(T), "x");
                Expression conversion =
                    Expression.Convert(Expression.Property(param, queryParameters.SortBy), typeof(object));
                var expression = Expression.Lambda<Func<T, object>>(conversion, param);

                query = queryParameters.SortDirection == SortDirection.Asc
                    ? query.OrderBy(expression)
                    : query.OrderByDescending(expression);
            }

            return query.Skip(queryParameters.Skip).Take(queryParameters.Take);
        }

        public static IQueryable<TEntity> ApplyFilters<TEntity, TFilters>(this IQueryable<TEntity> query,
            TFilters filters)
        {
            if (filters == null)
            {
                return query;
            }

            foreach (var property in typeof(TFilters).GetProperties())
            {
                var value = property.GetValue(filters);

                if (value == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof(string))
                {
                    query = query.StringPropertyContains(property.Name, value.ToString());
                    continue;
                }

                if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                {
                    query = query.BooleanPropertyEquals(property.Name, Convert.ToBoolean(value));
                    continue;
                }
            }

            return query;
        }

        public static IQueryable<T> StringPropertyContains<T>(this IQueryable<T> query, string propertyName,
            string value)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var getter = Expression.Property(parameter, propertyName);

            if (getter.Type != typeof(string))
            {
                throw new ArgumentException("Property must be a string");
            }

            var indexOf = Expression.Call
            (
                getter,
                "IndexOf",
                null,
                Expression.Constant(value, typeof(string)),
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase)
            );
            var like = Expression.GreaterThanOrEqual
            (
                indexOf,
                Expression.Constant(0)
            );

            return query.Where(Expression.Lambda<Func<T, bool>>(like, parameter));
        }

        public static IQueryable<T> BooleanPropertyEquals<T>(this IQueryable<T> query, string propertyName, bool value)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var expression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(parameter, propertyName),
                    Expression.Constant(value)
                ),
                parameter
            );
            return query.Where(expression);
        }

        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query, QueryParameters queryParameters,
            CancellationToken cancellationToken)
        {
            return query.ApplyQueryParameters(queryParameters).ToListAsync(cancellationToken);
        }

        public static Task<List<T>> ToListAsync<T, TFilters>(this IQueryable<T> query, QueryParameters queryParameters,
            TFilters filters, CancellationToken cancellationToken)
        {
            return query.ApplyFilters(filters).ApplyQueryParameters(queryParameters).ToListAsync(cancellationToken);
        }
    }
}