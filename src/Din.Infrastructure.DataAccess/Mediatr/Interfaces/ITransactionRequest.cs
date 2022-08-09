using MediatR;

namespace Din.Infrastructure.DataAccess.Mediatr.Interfaces
{
    public interface ITransactionRequest<out TResponse> : IRequest<TResponse>
    {
    }
}
