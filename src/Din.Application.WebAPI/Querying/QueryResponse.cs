using System.Collections.Generic;

namespace Din.Application.WebAPI.Querying
{
    public record struct QueryResponse<T>(IEnumerable<T> Items, int TotalCount)
    {
        public IEnumerable<T> Items { get; init; } = Items;
        public int TotalCount { get; init; } = TotalCount;
    }
}