using FluentValidation;

namespace Din.Domain.Commands.Accounts
{
    public class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
    {
        public ActivateAccountCommandValidator()
        {
            RuleFor(cmd => cmd.Code)
                .NotEmpty().WithMessage("The code cannot be empty")
                .Length(30).WithMessage("The code is exactly thirty characters long");
        }
    }
}
