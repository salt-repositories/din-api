using FluentValidation;

namespace Din.Domain.Commands.Accounts
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(cmd => cmd.Account.Username)
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(3).WithMessage("Username must be longer than three characters")
                .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters");

            RuleFor(cmd => cmd.Account.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Email must be a valid email");

            RuleFor(cmd => cmd.Account.Role)
                .NotNull().WithMessage("Role cannot be undefined");

            RuleFor(cmd => cmd.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must contain at least eight characters");
        }
    }
}
