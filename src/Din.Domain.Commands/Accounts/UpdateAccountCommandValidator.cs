using System.Linq;
using FluentValidation;

namespace Din.Domain.Commands.Accounts
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            When(cmd => cmd.Update.Operations.FirstOrDefault(operation => operation.path.Equals("password")) != null,
                () =>
                {
                    RuleFor(cmd =>
                            cmd.Update.Operations.FirstOrDefault(operation => operation.path.Equals("password")).value
                                .ToString().Length)
                        .GreaterThanOrEqualTo(8).WithMessage("The new password must at least be eight characters");
                });
        }
    }
}
