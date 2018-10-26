using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Din.Service.Config.Interfaces;
using Din.Service.Dto.Jwt;
using Din.Service.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Din.Service.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly ITokenConfig _config;

        public TokenService(ITokenConfig config)
        {
            _config = config;
        }


        public (bool valid, string token) CreateToken(TokenRequestDto data)
        {
            if (!data.ClientId.Equals(_config.ClientId) || !data.Secret.Equals(_config.Secret))
                return (false, null);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config.Issuer,
                _config.Issuer,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return (true, new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
