using Din.Domain.Models.Querying;
using MediatR;

namespace Din.Domain.Mediatr.Interfaces
{
    public interface IContentRetrievalRequest : IBaseRequest
    {
        public ContentFilters Filters { get; }
        public ContentQueryParameters ContentQueryParameters { get; }
    }
}
