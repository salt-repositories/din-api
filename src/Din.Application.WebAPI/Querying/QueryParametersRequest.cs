using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Querying
{
    public record struct QueryParametersRequest
    {
        public int? Skip { get; init; }
        public int? Take { get; init; }
        public string SortBy { get; init; }
        public SortDirection? SortDirection { get; init; }

        public static implicit operator QueryParameters(QueryParametersRequest request)
        {
            var queryParameters = new QueryParameters();
            var pageSize = (request.Take > QueryParameters.MaxPageSize 
                               ? QueryParameters.MaxPageSize
                               : request.Take) 
                           ?? QueryParameters.DefaultPageSize;

            queryParameters.WithPaging(request.Skip, pageSize);
            
            if (request.SortBy != null)
            {
                queryParameters.WithSorting(request.SortBy, request.SortDirection);
            }

            return queryParameters;
        }
    }
}
