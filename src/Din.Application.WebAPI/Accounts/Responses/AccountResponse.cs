using System;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Accounts.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public AccountRole Role { get; set; }
        public AccountResponseImage Image { get; set; }
    }

    public class AccountResponseImage
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
