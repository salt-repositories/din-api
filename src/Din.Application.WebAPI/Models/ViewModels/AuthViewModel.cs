namespace Din.Application.WebAPI.Models.ViewModels
{
    public class AuthViewModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
