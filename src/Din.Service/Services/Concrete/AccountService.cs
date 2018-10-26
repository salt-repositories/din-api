using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Dto.Context;
using Din.Service.Mapping.Profiles.Interfaces;
using Din.Service.Services.Abstractions;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : BaseService, IAccountService
    {
        private readonly DinContext _context;
        private readonly IMapper _mapper;

        public AccountService(DinContext context, IAccountMapProfile mapProfile)
        {
            _context = context;
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(mapProfile.GetType())));
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

        public async Task UpdateAccountAsync(AccountDto account)
        {
            _context.Account.Update(_mapper.Map<AccountEntity>(account));
            await _context.SaveChangesAsync();
        }    
    }
}