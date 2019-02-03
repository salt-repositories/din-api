using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess;
using Din.Infrastructure.DataAccess.Entities;
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

        public async Task<IEnumerable<AccountEntity>> GetAccountsAsync()
        {
            return await _context.Account.Include(a => a.Image).ToListAsync();
        }

        public async Task<AccountEntity> GetAccountByIdAsync(Guid id)
        {
            return await _context.Account.Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id));
        }

        public async Task<IEnumerable<AddedContentEntity>> GetAccountAddedContent(Guid id)
        {
            return await _context.AddedContent.Where(a => a.Account.Id.Equals(id)).ToListAsync();
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

        public async Task DeleteAccountById(Guid id)
        {
            _context.Account.Remove(await _context.Account.FirstAsync(a => a.Id.Equals(id)));
            await _context.SaveChangesAsync();
        }
    }
}