using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountRepository _repository;

        public CreateAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            request.Account.Hash = BC.HashPassword(request.Password);
            
            _repository.Insert(request.Account);

            return Task.FromResult(request.Account);
        }
    }
}
