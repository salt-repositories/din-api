using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Accounts
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public ActivateAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetAccountById(request.Identity, cancellationToken);

            if (account.Active)
            {
                throw new AccountActivationException("The account is already activated");
            }

            foreach (var code in account.Codes)
            {
                if (!BC.Verify(request.Code, code.Code))
                {
                    continue;
                }

                if (!code.Active)
                {
                    throw new AccountActivationException("The authorization code has expired");
                }

                account.Active = true;
                account.Codes.Remove(code);

                return Unit.Value;
            }

            throw new AccountActivationException("Invalid authorization code");
        }
    }
}
