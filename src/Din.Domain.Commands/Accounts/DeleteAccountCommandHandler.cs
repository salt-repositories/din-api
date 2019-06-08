using System.Threading;
using System.Threading.Tasks;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public DeleteAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetAccountById(request.Identity, cancellationToken);

            _repository.Delete(account);

            return Unit.Value;
        }
    }
}
