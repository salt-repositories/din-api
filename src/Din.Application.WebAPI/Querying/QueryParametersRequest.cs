using Din.Domain.Models.Querying;

namespace Din.Application.WebAPI.Querying
{
    public class QueryParametersRequest
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
