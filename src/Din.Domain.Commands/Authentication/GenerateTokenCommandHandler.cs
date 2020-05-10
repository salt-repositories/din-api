using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRefreshTokenRepository _tokenRepository;
        private readonly ITokenHelper _tokenManager;

        public GenerateTokenCommandHandler(IAccountRepository accountRepository, IRefreshTokenRepository tokenRepository, ITokenHelper tokenManager)
        {
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _tokenManager = tokenManager;
        }

        public async Task<TokenDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var account = string.IsNullOrEmpty(request.Credentials.Email)
                ? await _accountRepository.GetAccountByUsername(request.Credentials.Username, cancellationToken)
                : await _accountRepository.GetAccountByEmail(request.Credentials.Email, cancellationToken);

            if (account == null || !BC.Verify(request.Credentials.Password, account.Hash))
            {
                return new TokenDto {ErrorMessage = "Invalid credentials"};
            }

            if (!account.Active)
            {
                return new TokenDto {ErrorMessage = "Account is inactive, please reset your password"};
            }

            await _tokenRepository.Invoke(account.Id, cancellationToken);

            return new TokenDto
            {
                AccessToken = _tokenManager.GenerateJWtToken(account.Id, account.Role),
                ExpiresIn = 3600,
                RefreshToken = _tokenManager.GenerateRefreshToken(account.Id, DateTime.Now),
                TokenType = "Bearer"
            };
        }
    }
}