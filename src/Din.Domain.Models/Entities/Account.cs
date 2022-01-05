using System;
using System.Collections.Generic;

namespace Din.Domain.Models.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public AccountRole Role { get; set; }
        public bool Active { get; set; }
        public AccountImage Image { get; set; }
        public ICollection<AddedContent> AddedContent { get; set; }
        public ICollection<AccountAuthorizationCode> Codes { get; set; }
    }

    public class AccountImage : IScopedEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }

    public enum AccountRole
    {
        User,
        Moderator,
        Admin
    }

    public class AccountAuthorizationCode : IScopedEntity
    {
        public Guid Id { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Generated { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
