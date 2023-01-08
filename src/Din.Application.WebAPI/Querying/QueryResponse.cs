using System.Collections.Generic;
using Din.Domain.Queries.Querying;

namespace Din.Application.WebAPI.Querying
{
    public class QueryResponse<T>
    {
        public IEnumerable<T> Items { get; }
        public int TotalCount { get; }

        public QueryResponse(IEnumerable<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public static implicit operator QueryResponse<T>(QueryResult<T> queryResult)
            => new(queryResult.Items, queryResult.TotalCount);
    }
}
