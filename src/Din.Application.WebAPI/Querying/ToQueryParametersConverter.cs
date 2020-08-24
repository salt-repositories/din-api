using AutoMapper;
using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Querying
{
    public class ToQueryParametersConverter : ITypeConverter<QueryParametersRequest, QueryParameters>
    {
        public QueryParameters Convert(QueryParametersRequest source, QueryParameters destination, ResolutionContext context)
        {
            var queryParameters = new QueryParameters();

            var pageSize = (source.Take > QueryParameters.MaxPageSize 
                               ? QueryParameters.MaxPageSize
                               : source.Take) 
                           ?? QueryParameters.DefaultPageSize;

            queryParameters.WithPaging(source.Skip, pageSize);
            
            if (source.SortBy != null)
            {
                queryParameters.WithSorting(source.SortBy, source.SortDirection);
            }

            return queryParameters;
        }
    }
}
