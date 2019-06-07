namespace Din.Application.WebAPI.Models.Response
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
