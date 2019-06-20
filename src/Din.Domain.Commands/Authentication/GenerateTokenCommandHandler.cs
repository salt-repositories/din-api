using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenDto>
    {
        private readonly IAccountRepository _repository;
        private readonly IJwtConfig _config;

        public GenerateTokenCommandHandler(IAccountRepository repository, IJwtConfig config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<TokenDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var account = string.IsNullOrEmpty(request.Credentials.Email)
                ? await _repository.GetAccountByUsername(request.Credentials.Username, cancellationToken)
                : await _repository.GetAccountByEmail(request.Credentials.Email, cancellationToken);

            if (account == null)
            {
                return new TokenDto {ErrorMessage = "Invalid Credentials"};
            }

            if (!account.Active)
            {
                return new TokenDto {ErrorMessage = "Account is not activated"};
            }

            if (!BC.Verify(request.Credentials.Password, account.Hash))
            {
                return new TokenDto {ErrorMessage = "Invalid credentials"};
            }

            return new TokenDto
            {
                AccessToken = GenerateJWtToken(account.Id, account.Role),
                ExpiresIn = 3600,
                TokenType = "Bearer"
            };
        }

        private string GenerateJWtToken(Guid id, AccountRole role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                _config.Issuer,
                _config.Issuer,
                new List<Claim>
                {
                    new Claim("Identity", id.ToString()),
                    new Claim("Role", role.ToString())
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}