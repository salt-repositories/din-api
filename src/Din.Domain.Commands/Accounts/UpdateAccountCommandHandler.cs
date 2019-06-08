using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Accounts
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Account>
    {
        private readonly IAccountRepository _repository;

        public UpdateAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetAccountById(request.Identity, cancellationToken);
            
            request.Update.ApplyTo(account);

            _repository.Update(account);

            return account;
        }
    }
}
