using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Models.Entity;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Din.Domain.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly DinContext _context;

        public AccountService(DinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _context.Account.Include(a => a.Image).ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _context.Account.Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id));
        }

        public async Task<IEnumerable<AddedContent>> GetAccountAddedContent(Guid id)
        {
            return await _context.AddedContent.Where(a => a.Account.Id.Equals(id)).ToListAsync();
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task DeleteAccountById(Guid id)
        {
            _context.Account.Remove(await _context.Account.FirstAsync(a => a.Id.Equals(id)));
            await _context.SaveChangesAsync();
        }
    }
}