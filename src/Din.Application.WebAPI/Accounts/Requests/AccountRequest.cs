using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Accounts.Requests
{
    public class AccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountRole Role { get; set; }
    }
}
