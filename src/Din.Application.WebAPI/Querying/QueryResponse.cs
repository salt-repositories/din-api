using System.Collections.Generic;

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
    }
}
