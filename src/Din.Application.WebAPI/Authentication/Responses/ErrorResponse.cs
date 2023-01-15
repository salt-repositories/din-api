namespace Din.Application.WebAPI.Authentication.Responses
{
    public record struct ErrorResponse
    {
        public string Message { get; init; }
        public object Details { get; init; }
    }
}