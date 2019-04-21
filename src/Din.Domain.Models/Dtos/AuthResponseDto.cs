namespace Din.Domain.Models.Dtos
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
