using System.Threading.Tasks;
using Din.Service.Dtos;

namespace Din.Service.Services.Interfaces
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
        /// <returns>The generated claims principle for authorization</returns>
        Task<(bool success, string message, string token)> LoginAsync(AuthDto credentials);
    }
}
