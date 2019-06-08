using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
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
            Account account;

            try
            {
                account = await _repository.GetAccountByUsername(request.AuthenticationDetails.Username, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new AuthenticationException("Incorrect username");
            }

            if (!BC.Verify(request.AuthenticationDetails.Password, account.Hash))
            {
                throw new AuthenticationException("Incorrect password");
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

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
