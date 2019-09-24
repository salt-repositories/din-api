using System;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Authentication.Requests;
using Din.Application.WebAPI.Authentication.Responses;
using Din.Application.WebAPI.Middleware;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Commands.Authentication;
using Din.Domain.Exceptions.Concrete;
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
        /// <param name="credentials">Credentials</param>
        /// <returns>JWT token and Refresh token</returns>
        [AllowAnonymous, HttpPost("token")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse),400)]
        public async Task<IActionResult> LoginAsync([FromBody] CredentialRequest credentials)
        {
            var command = new GenerateTokenCommand(_mapper.Map<CredentialsDto>(credentials));
            var result = await _bus.Send(command);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new AuthenticationException(result.ErrorMessage);
            }

            return Ok(_mapper.Map<TokenResponse>(result));
        }

        /// <summary>
        /// Request JWT Token through a Refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Jwt Token and Refresh token</returns>
        [AllowAnonymous, HttpGet("refresh/{refreshToken}")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> RefreshAsync([FromRoute] string refreshToken)
        {
            var command = new RefreshTokenCommand(refreshToken, DateTime.Now);
            var result = await _bus.Send(command);

            return Ok(_mapper.Map<TokenResponse>(result));
        }

        /// <summary>
        /// Request authorization code
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpGet("authorization_code")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> RequestAuthorizationCode([FromQuery] string email)
        {
            var command = new SendAuthorizationCodeCommand(email);
            await _bus.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change the password of an account with a valid authorization code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost("change_password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> ChangeAccountPassword([FromBody] ChangeAccountPasswordRequest request)
        {
            var command = new ChangeAccountPasswordCommand(request.Email, request.Password, request.AuthorizationCode);
            await _bus.Send(command);

            return Ok();
        }

       
        #endregion endpoints
    }
}