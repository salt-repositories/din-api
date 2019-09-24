using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Authentication
{
    public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand>
    {
        private readonly IAccountRepository _repository;

        public ChangeAccountPasswordCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetAccountByEmail(request.Email, cancellationToken) ?? 
                          throw new NotFoundException("There is no account registered by that email", new { request.Email });

            foreach (var code in account.Codes)
            {
                if (!BC.Verify(request.AuthorizationCode, code.Code))
                {
                    continue;
                }

                if (!code.Active)
                {
                    throw new AccountChangePasswordException("The authorization code has expired");
                }

                account.Hash = BC.HashPassword(request.Password);
                account.Active = true;
                account.Codes.Remove(code);

                return Unit.Value;
            }
            
            throw new AccountChangePasswordException("Invalid authorization code");
        }
    }
}