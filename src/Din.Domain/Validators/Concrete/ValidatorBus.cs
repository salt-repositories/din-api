using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Validators.Interfaces;

namespace Din.Domain.Validators.Concrete
{
    public class ValidatorBus<T> : IValidatorBus<T>
    {
        private readonly IValidator<T>[] _validators;

        public ValidatorBus(IValidator<T>[] validators)
        {
            _validators = validators;
        }

        public async Task ValidateAsync(T obj)
        {
            ICollection<Task> tasks = _validators.Select(validator => validator.ValidateAsync(obj)).ToList();

            await Task.WhenAll(tasks);
        }
    }
}
