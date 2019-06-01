using System.Threading.Tasks;
using Din.Domain.Models.Dtos;

namespace Din.Domain.Services.Interfaces
{
    /// <summary>
    /// Authentication service for the corresponding controller.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// User Login method, authenticates the supplied parameters
        /// </summary>
        /// <param name="credentials">Login credentials containing the username and password</param>
        /// <param name="userAgent"></param>
        /// <param name="ip"></param>
        /// <returns>The generated claims principle for authorization</returns>
        Task<AuthResponseDto> LoginAsync(AuthRequestDto credentials, string userAgent, string ip);
    }
}
