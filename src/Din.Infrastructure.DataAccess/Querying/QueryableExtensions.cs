using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public static IQueryable<T> ApplyFilters<T, K>(this IQueryable<T> query, K filters)
        {
            foreach (var propertyInfo in filters.GetType().GetProperties())
            {
                var param = Expression.Parameter(typeof(T), "x");
                var member = Expression.Property(param, propertyInfo.Name);

                UnaryExpression valueExpression;

                try
                {
                    var value = propertyInfo.GetValue(filters) ?? throw new NullReferenceException();
                    valueExpression = GetValueExpression(propertyInfo.Name, value, param);
                }
                catch (NullReferenceException)
                {
                    continue;
                }

                Expression body = Expression.Equal(member, valueExpression);
                var expression = Expression.Lambda<Func<T, bool>>(body, param);

                query = query.Where(expression);
            }

            return query;
        }
        private static UnaryExpression GetValueExpression(string propertyName, object value, ParameterExpression param)
        {
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
            {
                throw new NotSupportedException();
            }

            var constant = Expression.Constant(value);

            return Expression.Convert(constant, propertyType);
        }
    }
}
