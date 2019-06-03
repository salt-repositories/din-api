namespace Din.Domain.Models.Querying
{
    public class QueryParameters<T>
    {
        public const int DEFAULT_PAGE_SIZE = 20;
        public const int MAX_PAGE_SIZE = 50;

        public int Skip { get; private set; }
        public int Take { get; private set; } = DEFAULT_PAGE_SIZE; //TODO Fix mapping
        public string SortBy { get; private set; }
        public SortDirection? SortDirection { get; private set; }

        public QueryParameters()
        {
            Skip = 0;
            Take = DEFAULT_PAGE_SIZE;
        }

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
