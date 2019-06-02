using System.Threading.Tasks;
using Din.Domain.Models.Entities;
using FluentValidation;

namespace Din.Domain.Validators.Concrete
{
    public class AccountValidator : AbstractValidator<Account>, IValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Username)
                .NotEmpty().WithMessage("Username cannot empty or null")
                .MinimumLength(3).WithMessage("Username must be longer then three characters")
                .MaximumLength(40).WithMessage("Username cannot be longer then 40 characters");
            RuleFor(a => a.Hash)
                .NotEmpty().WithMessage("Hash cannot empty or null")
                .MinimumLength(5).WithMessage("Hash must be longer then three characters");
            RuleFor(a => a.Role)
                .NotNull().WithMessage("Role cannot be null");
        }

        public async Task ValidateAsync(Account obj)
        {
            await this.ValidateAndThrowAsync(obj);
        }
    }
}
