using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenHelper _tokenHelper;

        public RefreshTokenCommandHandler(IRefreshTokenRepository tokenRepository, IAccountRepository accountRepository,
            ITokenHelper tokenHelper)
        {
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _tokenRepository.Get(request.RefreshToken, cancellationToken) ?? throw new AuthenticationException("Invalid Refresh token");

            await _tokenRepository.Invoke(refreshToken, cancellationToken);

            if (refreshToken.CreationDate.AddDays(30) < request.RequestDate)
            {
                throw new AuthenticationException("Refresh token expired");
            }

            var account = await _accountRepository.GetAccountById(refreshToken.AccountIdentity, cancellationToken);

            return new TokenDto
            {
                AccessToken = _tokenHelper.GenerateJWtToken(account.Id, account.Role),
                ExpiresIn = 3600,
                RefreshToken = _tokenHelper.GenerateRefreshToken(account.Id, refreshToken.CreationDate),
                TokenType = "Bearer"
            };
        }
    }
}