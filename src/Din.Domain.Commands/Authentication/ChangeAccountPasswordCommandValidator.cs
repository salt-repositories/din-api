using FluentValidation;

namespace Din.Domain.Commands.Authentication
{
    public class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
    {
        public ChangeAccountPasswordCommandValidator()
        {
            RuleFor(cmd => cmd.Email)
                .NotEmpty().WithMessage("Request must contain an email")
                .EmailAddress().WithMessage("Must be a valid email");
            RuleFor(cmd => cmd.Password)
                .NotEmpty().WithMessage("Request must contain an password");
            RuleFor(cmd => cmd.Password.Length)
                .GreaterThanOrEqualTo(8).WithMessage("The new password must be at least eight characters");
            RuleFor(cmd => cmd.AuthorizationCode)
                .NotEmpty().WithMessage("The code cannot be empty")
                .Length(30).WithMessage("The code is exactly thirty characters long");
        }
    }
}