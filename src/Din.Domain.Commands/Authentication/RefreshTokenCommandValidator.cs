using FluentValidation;

namespace Din.Domain.Commands.Authentication
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(cmd => cmd.RefreshToken)
                .NotEmpty().WithMessage("The refresh token cannot be empty");
        }
    }
}
