using System.Collections.Generic;

namespace Din.Data.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountEntity Account { get; set; }
    }

    public class AccountEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRoll Role { get; set; }
        public UserEntity User { get; set; }
        public AccountImageEntity Image { get; set; }
        public List<AddedContentEntity> AddedContent { get; set; }
        public int UserRef { get; set; }
    }

    public class AccountImageEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public AccountEntity Account { get; set; }
        public int AccountRef { get; set; }
    }

    public enum AccountRoll
    {
        User,
        Moderator,
        Admin
    }
}
