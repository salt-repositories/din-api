using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Managers.Interfaces;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using Din.Domain.Stores.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Din.Domain.Managers.Concrete
{
    public class TokenManager : ITokenManager

    {
        private readonly IRefreshTokenStore _store;
        private readonly IJwtConfig _config;

        public TokenManager(IRefreshTokenStore store, IJwtConfig config)
        {
            _store = store;
            _config = config;
        }

        public string GenerateJWtToken(Guid id, AccountRole role)
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

        public string GenerateRefreshToken(Guid id, DateTime creationDate)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[128];
                provider.GetBytes(bytes);

                var token = Convert.ToBase64String(bytes)
                    .Replace("+", "")
                    .Replace("-", "")
                    .Replace("=", "")
                    .Replace("/", "");

                _store.Insert(new RefreshTokenDto
                {
                    Token = token,
                    AccountIdentity = id,
                    CreationDate = creationDate
                });

                return token;
            }
        }
    }
}