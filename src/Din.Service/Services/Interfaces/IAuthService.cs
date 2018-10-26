using System.Security.Claims;
using System.Threading.Tasks;
using Din.Data.Entities;
using Din.Service.Dto.Auth;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// Authentication service for the corresponding controller.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// User Login method, authenticates the supplied parameters.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password in hash (BCrypt) format</param>
        /// <returns>The generated claims principle for authorization</returns>
        Task<(bool status, string message)> LoginAsync(CredentialsDto credentials);

        /// <summary>
        /// Logging of the LoginAsync attempt.
        /// </summary>
        /// <param name="username">Username supplied in the user input.</param>
        /// <param name="userAgentString">user-agent string supplied by the browser.</param>
        /// <param name="publicIp">Users public ip.</param>
        /// <param name="status">Status returned by LoginAsync.</param>
        /// <returns></returns>
        Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status);
    }
}
