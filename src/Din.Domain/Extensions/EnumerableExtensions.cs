using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public static IEnumerable<T> ApplyFilter<T>
        (
            this IEnumerable<T> collection,
            string propertyName,
            string value
        )
        {
            var param = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(param, propertyName);
            var valueExpression = GetValueExpression(propertyName, value, param);
            Expression body = Expression.Equal(member, valueExpression);
            var expresion = Expression.Lambda<Func<T, bool>>(body, param);

            return collection.Where(expresion.Compile());
        }

        private static UnaryExpression GetValueExpression(string propertyName, string value, ParameterExpression param)
        {
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
            {
                throw new NotSupportedException();
            }

            var propertyValue = converter.ConvertFromInvariantString(value);
            var constant = Expression.Constant(propertyValue);
            
            return Expression.Convert(constant, propertyType);
        }
    }
    
}