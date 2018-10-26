using System.Collections.Generic;
using Din.Data.Entities;

namespace Din.Service.Dto.Context
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AccountDto
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public AccountRoll Role { get; set; }
        public UserDto User { get; set; }
        public AccountImageDto Image { get; set; }
        public ICollection<AddedContentDto> AddedContent { get; set; }
    }

    public class AccountImageDto
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
