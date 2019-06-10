using Din.Domain.Models.Querying;

namespace Din.Domain.Queries.Querying
{
    public abstract class RequestWithQueryParameters<T>
    {
        public QueryParameters<T> QueryParameters { get; }

        protected RequestWithQueryParameters(QueryParameters<T> queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }
}
