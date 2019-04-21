using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entity
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRoll Role { get; set; }
        public AccountImage Image { get; set; }
        public List<AddedContent> AddedContent { get; set; }

    }

    public class AccountImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }

    public enum AccountRoll
    {
        User,
        Moderator,
        Admin
    }
}
