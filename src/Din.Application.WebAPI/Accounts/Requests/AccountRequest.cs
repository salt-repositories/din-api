using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Accounts.Requests
{
    public record AccountRequest
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public AccountRole Role { get; init; }

        public static implicit operator Account(AccountRequest request) => new()
        {
            Username = request.Username,
            Email = request.Email,
            Role = request.Role
        };
    }
}
