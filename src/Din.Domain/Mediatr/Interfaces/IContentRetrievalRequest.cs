using MediatR;

namespace Din.Domain.Mediatr.Interfaces
{
    public interface IContentRetrievalRequest : IBaseRequest
    {
        public bool Plex { get; }
    }
}
