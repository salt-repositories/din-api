namespace Din.Application.WebAPI.Authentication.Requests
{
    public record struct ChangeAccountPasswordRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string AuthorizationCode { get; init; }
    }
}