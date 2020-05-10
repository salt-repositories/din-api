﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Din.Domain.Helpers.Concrete
{
    public class TokenHelper : ITokenHelper

    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtConfig _config;

        public TokenHelper(IRefreshTokenRepository refreshTokenRepository, IJwtConfig config)
        {
            _refreshTokenRepository = refreshTokenRepository;
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
            var token = RandomCodeGenerator.GenerateRandomCode(256);

            _refreshTokenRepository.Insert(new RefreshToken
            {
                Token = token,
                AccountIdentity = id,
                CreationDate = creationDate
            });

            return token;
        }
    }
}