using System;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Models.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public AccountRole Role { get; set; }
        public AccountImageViewModel Image { get; set; }
    }

    public class AccountImageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
