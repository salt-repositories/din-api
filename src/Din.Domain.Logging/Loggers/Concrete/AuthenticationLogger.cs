using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Context;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Logging.Loggers.Interfaces;
using Din.Domain.Logging.Requests;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using UAParser;

namespace Din.Domain.Logging.Loggers.Concrete
{
    public class AuthenticationLogger<TRequest, TResponse> : IRequestLogger<TRequest, TResponse>
        where TRequest : IAuthenticationRequest
    {
        private readonly ILoginAttemptRepository _repository;
        private readonly IRequestContext _context;
        private readonly IIpStackClient _client;
        private readonly IMapper _mapper;

        public AuthenticationLogger(ILoginAttemptRepository repository, IRequestContext context, IIpStackClient client,
            IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _client = client;
            _mapper = mapper;
        }

        public async Task Log(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            var ipAddress = _context.GetRequestIpAsString();
            LoginLocation location = null;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                try
                {
                    location = _mapper.Map<LoginLocation>(await _client.GetLocationAsync(ipAddress, cancellationToken));
                    location = await _repository.FindLoginLocationByCoordinates(location.Latitude, location.Longitude) ?? location;
                }
                catch
                {
                    location = null;
                }
            }

            var clientInformation = Parser.GetDefault().Parse(_context.GetUserAgentAsString());

            var loginAttempt = new LoginAttempt
            {
                Username = request.Credentials.Username,
                Device = clientInformation.Device.Family,
                Os = clientInformation.OS.Family,
                Browser = clientInformation.UA.Family,
                PublicIp = ipAddress,
                DateAndTime = DateTime.Now,
                Location = location,
                Status = response == null ? LoginStatus.Failed : LoginStatus.Success
            };

            _repository.Insert(loginAttempt);

            if (response == null)
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }
    }
}