using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.JsonPatch.Operations;
using BC = BCrypt.Net.BCrypt;

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

            foreach (var operation in request.Update.Operations)
            {
                if (!operation.path.Equals("password") || !operation.OperationType.Equals(OperationType.Replace))
                {
                    continue;
                }

                account.Hash = BC.HashPassword(operation.value.ToString());
                request.Update.Operations.Remove(operation);
                break;
            }
            
            request.Update.ApplyTo(account);

            _repository.Update(account);

            return account;
        }
    }
}
