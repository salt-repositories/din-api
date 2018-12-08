using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data.Entities;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// Service for corresponding account controller
    /// </summary>
    public interface IAccountService
    {
        Task<IEnumerable<AccountEntity>> GetAccountsAsync();
        /// <summary>
        /// Gets the account data associated with the session
        /// </summary>
        /// <param name="id">Account id, stored in the session</param>
        /// <returns>ViewModel containing the account data</returns>
        Task<AccountEntity> GetAccountByIdAsync(Guid id);

        Task<AccountEntity> CreateAccountAsync(AccountEntity account);

        Task<AccountEntity> UpdateAccountAsync(AccountEntity account);
        Task DeleteAccountById(Guid id);

    }
}
