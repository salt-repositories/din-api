using FluentValidation;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
    {
        public GenerateTokenCommandValidator()
        {
            RuleFor(cmd => cmd.Credentials.Username)
                .NotEmpty().WithMessage("Request must contain a username");

            RuleFor(cmd => cmd.Credentials.Password)
                .NotEmpty().WithMessage("Request must contain a valid password")
                .MinimumLength(8).WithMessage("The password is always at least eight characters");
        }
    }
}
