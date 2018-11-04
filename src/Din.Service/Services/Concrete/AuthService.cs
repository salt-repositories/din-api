using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Config.Interfaces;
using Din.Service.Dto.Auth;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UAParser;

namespace Din.Service.Services.Concrete
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

        public async Task<(bool success, string message, string token)> LoginAsync(CredentialsDto credentials)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.Username.Equals(credentials.Username));

                if (!BCrypt.Net.BCrypt.Verify(credentials.Password, accountEntity.Hash))
                {
                    //TODO log login attempt
                    return (false, "Password Incorrect", null);
                }

                //TODO log login attempt
                return (true, null, GenerateToken());
            }
            catch (InvalidOperationException)
            {
                return (false, "Username Incorrect", null);
            }
        }

        private string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(_config.Issuer,
                _config.Issuer,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
        {
            var clientInfo = Parser.GetDefault().Parse(userAgentString);
            var loginAttemptDto = new LoginAttemptDto
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
                loginAttemptDto.Location = _mapper.Map<LoginLocationDto>(await _ipStackClient.GetLocation(publicIp));

                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttemptEntity>(loginAttemptDto));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttemptEntity>(loginAttemptDto));
                await _context.SaveChangesAsync();
            }
        }
    }
}