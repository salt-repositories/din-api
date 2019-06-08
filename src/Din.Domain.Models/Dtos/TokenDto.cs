namespace Din.Domain.Models.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
