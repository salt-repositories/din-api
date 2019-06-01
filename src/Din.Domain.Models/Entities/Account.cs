using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRole Role { get; set; }
        public AccountImage Image { get; set; }
        public ICollection<AddedContent> AddedContent { get; set; }

    }

    public class AccountImage
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }

    public enum AccountRole
    {
        User,
        Moderator,
        Admin
    }
}
