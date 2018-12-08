using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Din.Data.Entities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRoll Role { get; set; }
        public AccountImageEntity Image { get; set; }
        public List<AddedContentEntity> AddedContent { get; set; }
    }

    public class AccountImageEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public AccountEntity Account { get; set; }
        public Guid AccountRef { get; set; }
    }

    public enum AccountRoll
    {
        User,
        Moderator,
        Admin
    }
}
