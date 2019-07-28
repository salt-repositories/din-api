using FluentValidation;

namespace Din.Domain.Commands.Authentication
{
    public class SendAuthorizationCodeCommandValidator : AbstractValidator<SendAuthorizationCodeCommand>
    {
        public SendAuthorizationCodeCommandValidator()
        {
            RuleFor(cmd => cmd.Email)
                .NotEmpty().WithMessage("Request must contain an email")
                .EmailAddress().WithMessage("Must be a valid email");
        }
    }
}
