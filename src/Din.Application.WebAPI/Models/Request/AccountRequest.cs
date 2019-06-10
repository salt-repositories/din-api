using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Models.Request
{
    public class AccountRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountRole Role { get; set; }
    }
}
