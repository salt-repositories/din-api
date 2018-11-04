using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly DinContext _context;

        public AccountService(DinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountEntity>> GetAccountsAsync()
        {
            return await _context.Account.Include(a => a.User).Include(a => a.Image).ToListAsync();
        }

        public async Task<AccountEntity> GetAccountByIdAsync(int id)
        {
            return await _context.Account.Include(a => a.User).Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id));
        }

        public async Task<AccountEntity> CreateAccountAsync(AccountEntity account)
        {
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountEntity> UpdateAccountAsync(AccountEntity account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }
    }
}