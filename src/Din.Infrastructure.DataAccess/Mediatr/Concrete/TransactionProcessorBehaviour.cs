using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Infrastructure.DataAccess.Mediatr.Concrete
{
    public class TransactionProcessorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : ITransactionRequest<TResponse>
    {
        private readonly DinContext _context;

        public TransactionProcessorBehaviour(DinContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            var response = await next();
                
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
                
            return response;
        }
    }
}
