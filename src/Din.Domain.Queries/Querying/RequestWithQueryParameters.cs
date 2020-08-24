using Din.Domain.Models.Querying;

namespace Din.Domain.Queries.Querying
{
    public abstract class RequestWithQueryParameters
    {
        public QueryParameters QueryParameters { get; }

        protected RequestWithQueryParameters(QueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }
}
