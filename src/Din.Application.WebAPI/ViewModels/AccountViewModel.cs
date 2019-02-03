using System;
using Din.Infrastructure.DataAccess.Entities;

namespace Din.Application.WebAPI.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public AccountRoll Role { get; set; }
        public AccountImageViewModel Image { get; set; }
    }

    public class AccountImageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
