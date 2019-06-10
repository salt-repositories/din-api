using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Authentication.Requests;
using Din.Application.WebAPI.Authentication.Responses;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Commands.Authentication;
using Din.Domain.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Authentication
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("authentication")]
    [ControllerName("Authentication")]
    [Produces("application/json")]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        #region fields

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion fields

        #region constructors

        public AuthenticationController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Request JWT Token
        /// </summary>
        /// <param name="credentials">Token Credentials</param>
        /// <returns>Authentication response</returns>
        [AllowAnonymous, HttpPost("token")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginAsync([FromBody] CredentialRequest credentials)
        {
            var command = new GenerateTokenCommand(_mapper.Map<CredentialsDto>(credentials));
            var result = await _bus.Send(command);

            return Ok(_mapper.Map<TokenResponse>(result));
        }
       
        #endregion endpoints
    }
}