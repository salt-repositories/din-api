using AutoMapper;
using Din.Application.WebAPI.Querying;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Mapping.Querying.Converters
{
    public class ToQueryParametersConverter<T> : ITypeConverter<QueryParametersRequest, QueryParameters<T>>
    {
        public QueryParameters<T> Convert(QueryParametersRequest source, QueryParameters<T> destination, ResolutionContext context)
        {
            var queryParameters = new QueryParameters<T>();

            var pageSize = (source.Take > QueryParameters<T>.MaxPageSize 
                               ? QueryParameters<T>.MaxPageSize
                               : source.Take) 
                           ?? QueryParameters<T>.DefaultPageSize;

            queryParameters.WithPaging(source.Skip, pageSize);
            
            if (source.SortBy != null)
            {
                queryParameters.WithSorting(source.SortBy, source.SortDirection);
            }

            return queryParameters;
        }
    }
}
