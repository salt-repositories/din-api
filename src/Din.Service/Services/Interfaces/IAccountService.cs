using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Dto.Context;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// Service for corresponding account controller
    /// </summary>
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAccountsAsync();
        /// <summary>
        /// Gets the account data associated with the session
        /// </summary>
        /// <param name="id">Account id, stored in the session</param>
        /// <returns>ViewModel containing the account data</returns>
        Task<AccountDto> GetAccountByIdAsync(int id);

        Task<AccountDto> CreateAccountAsync(AccountDto account);

        Task UpdateAccountAsync(AccountDto account);
    }
}
