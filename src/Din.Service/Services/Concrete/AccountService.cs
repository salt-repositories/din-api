using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly DinContext _context;
        private readonly IMapper _mapper;

        public AccountService(DinContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
        {
            return _mapper.Map<IEnumerable<AccountDto>>(await _context.Account.Include(a => a.User).Include(a => a.Image).ToListAsync());
        }

        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            return _mapper.Map<AccountDto>(await _context.Account.Include(a => a.User).Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id)));
        }

        public async Task<AccountDto> CreateAccountAsync(AccountDto account)
        {
            await _context.Account.AddAsync(_mapper.Map<AccountEntity>(account));
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountDto> UpdateAccountAsync(int id, JsonPatchDocument<AccountDto> data)
        {
            var account = await _context.Account.Include(a => a.User).Include(a => a.Image)
                .FirstAsync(a => a.Id.Equals(id));
            var entityData = _mapper.Map<JsonPatchDocument<AccountEntity>>(data);

            entityData.ApplyTo(account);

            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(account);
        }    
    }
}