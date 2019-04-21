using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.Models.Entity;
using Din.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Din.Domain.Services.Abstractions
{
    public abstract class ContentService
    {
        private readonly DinContext _context;
        protected readonly IMapper Mapper;

        protected ContentService(DinContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
        }

        protected async Task LogContentAdditionAsync(string title, Guid accountId, ContentType type, int foreignId, int systemId)
        {
            var account = await _context.Account.FirstAsync(a => a.Id.Equals(accountId));

            if(account.AddedContent == null)
                account.AddedContent = new List<AddedContent>();

            _context.Attach(account);
            account.AddedContent.Add(new AddedContent()
            {
                ForeignId = foreignId,
                SystemId = systemId,
                Title = title,
                DateAdded = DateTime.Now,
                Status = ContentStatus.Queued,
                Account = account,
                Type = type
            });
                
            await _context.SaveChangesAsync();
        }

        protected string GenerateTitleSlug(string title, DateTime date)
        {
            return $"{title.ToLower().Replace(" ", "-")}-{date.Year.ToString().ToLower()}";
        }     
    }
}