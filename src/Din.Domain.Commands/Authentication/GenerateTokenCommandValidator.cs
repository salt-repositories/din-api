using FluentValidation;

namespace Din.Domain.Commands.Authentication
{
    public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
    {
        public GenerateTokenCommandValidator()
        {
            When(cmd => string.IsNullOrEmpty(cmd.Credentials.Email), () =>
            {
                RuleFor(cmd => cmd.Credentials.Username)
                    .NotEmpty().WithMessage("Request must contain a username");
            });

            When(cmd => string.IsNullOrEmpty(cmd.Credentials.Username), () =>
            {
                RuleFor(cmd => cmd.Credentials.Email)
                    .NotEmpty().WithMessage("Request must contain a email");
            });

            RuleFor(cmd => cmd.Credentials.Password)
                .NotEmpty().WithMessage("Request must contain a valid password")
                .MinimumLength(8).WithMessage("The password is always at least eight characters");
        }
    }
}
