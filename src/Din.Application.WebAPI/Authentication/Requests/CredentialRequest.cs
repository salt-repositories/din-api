using Din.Domain.Models.Dtos;

namespace Din.Application.WebAPI.Authentication.Requests
{
    public record struct CredentialRequest
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }

        public static implicit operator CredentialsDto(CredentialRequest request) => new()
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };
    }
}
