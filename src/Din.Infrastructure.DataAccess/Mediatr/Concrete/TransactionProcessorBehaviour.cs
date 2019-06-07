using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Mediatr.Interfaces;
using MediatR;

namespace Din.Infrastructure.DataAccess.Mediatr.Concrete
{
    public class TransactionProcessorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionRequest
    {
        private readonly DinContext _context;

        public TransactionProcessorBehaviour(DinContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var response = await next();
                
                await _context.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                
                return response;
            }
        }
    }
}
