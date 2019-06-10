namespace Din.Domain.Models.Querying
{
    public class QueryParameters<T>
    {
        public const int DefaultPageSize = 20;
        public const int MaxPageSize = 50;

        public int Skip { get; set; }
        public int Take { get; set; } = DefaultPageSize;
        public string SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }


        public QueryParameters<T> WithPaging(int? skip, int? take)
        {
            if (skip.HasValue)
            {
                Skip = skip.Value;
            }

            if (take.HasValue)
            {
                Take = take.Value;
            }

            return this;
        }

        public QueryParameters<T> WithSorting(string sortBy, SortDirection? sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection ?? Querying.SortDirection.Asc;
            return this;
        }
    }
}
