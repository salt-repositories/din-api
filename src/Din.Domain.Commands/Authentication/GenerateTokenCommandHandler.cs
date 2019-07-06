using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Managers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Domain.Stores.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenDto>
    {
        private readonly IAccountRepository _repository;
        private readonly IRefreshTokenStore _store;
        private readonly ITokenManager _tokenManager;

        public GenerateTokenCommandHandler(IAccountRepository repository, IRefreshTokenStore store, ITokenManager tokenManager)
        {
            _repository = repository;
            _store = store;
            _tokenManager = tokenManager;
        }

        public async Task<TokenDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var account = string.IsNullOrEmpty(request.Credentials.Email)
                ? await _repository.GetAccountByUsername(request.Credentials.Username, cancellationToken)
                : await _repository.GetAccountByEmail(request.Credentials.Email, cancellationToken);

            if (account == null || !BC.Verify(request.Credentials.Password, account.Hash))
            {
                return new TokenDto {ErrorMessage = "Invalid credentials"};
            }

            _store.Invoke(account.Id);

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