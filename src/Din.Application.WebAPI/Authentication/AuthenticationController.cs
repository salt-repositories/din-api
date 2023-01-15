using System;
using System.Threading.Tasks;
using Din.Application.WebAPI.Authentication.Requests;
using Din.Application.WebAPI.Authentication.Responses;
using Din.Application.WebAPI.Controller;
using Din.Application.WebAPI.Controller.Versioning;
using Din.Domain.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Authentication
{
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("authentication")]
    [ControllerName("Authentication")]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _bus;

        public AuthenticationController(IMediator bus)
        {
            _bus = bus;
        }

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
            var command = new GenerateTokenCommand(credentials);
            var result = await _bus.Send(command);

            return Ok<TokenResponse>(result);
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

            return Ok<TokenResponse>(result);
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
            await _bus.Send(new SendAuthorizationCodeCommand(email));
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