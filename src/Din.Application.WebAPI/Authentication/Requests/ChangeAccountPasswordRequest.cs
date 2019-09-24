namespace Din.Application.WebAPI.Authentication.Requests
{
    public class ChangeAccountPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthorizationCode { get; set; }
    }
}