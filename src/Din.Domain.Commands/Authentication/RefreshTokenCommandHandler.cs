using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Managers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Domain.Stores.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IRefreshTokenStore _store;
        private readonly IAccountRepository _repository;
        private readonly ITokenManager _tokenManager;

        public RefreshTokenCommandHandler(IRefreshTokenStore store, IAccountRepository repository,
            ITokenManager tokenManager)
        {
            _store = store;
            _repository = repository;
            _tokenManager = tokenManager;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _store.Get(request.RefreshToken) ?? throw new AuthenticationException("Invalid RefreshToken");

            _store.Invoke(refreshToken);

            if (refreshToken.CreationDate.AddDays(30) < request.RequestDate)
            {
                throw new AuthenticationException("Refresh token expired");
            }

            var account = await _repository.GetAccountById(refreshToken.AccountIdentity, cancellationToken);

            return new TokenDto
            {
                AccessToken = _tokenManager.GenerateJWtToken(account.Id, account.Role),
                ExpiresIn = 3600,
                RefreshToken = _tokenManager.GenerateRefreshToken(account.Id, refreshToken.CreationDate),
                TokenType = "Bearer"
            };
        }
    }
}