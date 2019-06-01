using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Domain.Models.Entities;

namespace Din.Domain.Services.Interfaces
{
    /// <summary>
    /// Service for corresponding account controller
    /// </summary>
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<AddedContent>> GetAccountAddedContent(Guid id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountById(Guid id);
    }
}
