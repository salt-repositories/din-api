using Din.Domain.Models.Dtos;

namespace Din.Application.WebAPI.Authentication.Responses
{
    public record struct TokenResponse
    {
        public string AccessToken { get; init; }
        public int ExpiresIn { get; init; }
        public string RefreshToken { get; init; }
        public string TokenType { get; init; }

        public static implicit operator TokenResponse(TokenDto token) => new()
        {
            AccessToken = token.AccessToken,
            ExpiresIn = token.ExpiresIn,
            RefreshToken = token.RefreshToken,
            TokenType = token.TokenType
        };
    }
}