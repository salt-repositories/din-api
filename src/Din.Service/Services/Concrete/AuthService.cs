using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Dto.Auth;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Din.Service.Utils;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;
using UAParser;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;
        private readonly IIpStackClient _ipStackClient;
        private readonly IMapper _mapper;

        public AuthService(DinContext context, IIpStackClient ipStackClient, IMapper mapper)
        {
            _context = context;
            _ipStackClient = ipStackClient;
            _mapper = mapper;
        }

        public async Task<(bool status, string message)> LoginAsync(CredentialsDto credentials)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.Username.Equals(credentials.Username));

                if (!BCrypt.Net.BCrypt.Verify(credentials.Password, accountEntity.Hash))
                    return (false, "Password Incorrect");

                return (true, "Credentials are valid");
            }
            catch (InvalidOperationException)
            {
                return (false, "Username Incorrect");
            }
        }

        public async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
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