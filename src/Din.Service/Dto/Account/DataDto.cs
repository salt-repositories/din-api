using System.Collections.Generic;
using Din.Service.Dto.Context;

namespace Din.Service.Dto.Account
{
    public class DataDto
    {
        public UserDto User { get; set; }
        public AccountDto Account { get; set; }
        public IEnumerable<AddedContentDto> AddedContent { get; set; }
    }
}
