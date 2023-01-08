using System;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Accounts.Responses
{
    public record struct AccountResponse
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public AccountRole Role { get; init; }
        public bool Active { get; init; }

        public static implicit operator AccountResponse(Account account) => new()
        {
            Id = account.Id,
            Username = account.Username,
            Email = account.Email,
            Role = account.Role,
            Active = account.Active,
        };
    }

}
