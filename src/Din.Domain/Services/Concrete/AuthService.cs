using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.Clients.Configurations.Interfaces;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Exceptions;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UAParser;

namespace Din.Domain.Services.Concrete
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;
        private readonly IIpStackClient _ipStackClient;
        private readonly IMapper _mapper;
        private readonly ITokenConfig _config;

        public AuthService(DinContext context, IIpStackClient ipStackClient, IMapper mapper, ITokenConfig config)
        {
            _context = context;
            _ipStackClient = ipStackClient;
            _mapper = mapper;
            _config = config;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto credentials, string userAgent, string ip)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.Username.Equals(credentials.Username));

                if (!BCrypt.Net.BCrypt.Verify(credentials.Password, accountEntity.Hash))
                {
                    await LogLoginAttempt(accountEntity.Username, userAgent, ip, LoginStatus.Failed);

                    throw new AuthenticationException("Password incorrect");
                }

                await LogLoginAttempt(accountEntity.Username, userAgent, ip, LoginStatus.Success);

                return new AuthResponseDto
                {
                    AccessToken = GenerateToken(accountEntity.Id, accountEntity.Role),
                    ExpiresIn = 3600,
                    TokenType = "Bearer"
                };
            }
            catch (InvalidOperationException)
            {
                throw new AuthenticationException("Username incorrect");
            }
        }

        private string GenerateToken(Guid id, AccountRole role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(_config.Issuer,
                _config.Issuer,
                new List<Claim>
                {
                    new Claim("Identity", id.ToString()),
                    new Claim("Role", role.ToString())
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
        {
            var clientInfo = Parser.GetDefault().Parse(userAgentString);
            var loginAttempt = new LoginAttempt()
            {
                Username = username,
                Device = clientInfo.Device.Family,
                Os = clientInfo.OS.Family,
                Browser = clientInfo.UA.Family,
                PublicIp = publicIp,
                DateAndTime = DateTime.Now,
                Status = status
            };

            try
            {
                loginAttempt.Location = _mapper.Map<LoginLocation>(await _ipStackClient.GetLocation(publicIp));

                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttempt>(loginAttempt));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttempt>(loginAttempt));
                await _context.SaveChangesAsync();
            }
        }
    }
}