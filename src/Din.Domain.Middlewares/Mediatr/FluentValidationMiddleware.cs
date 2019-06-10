using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MediatR.Pipeline;

namespace Din.Domain.Middlewares.Mediatr
{
    public class FluentValidationMiddleware<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IBaseRequest
    {
        private readonly IValidator<TRequest>[] _validators;

        public FluentValidationMiddleware(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return;
            }

            var context = new ValidationContext(request);
            var failures = (await Task.WhenAll(_validators
                .Select(validator => validator.ValidateAsync(context, cancellationToken))).ConfigureAwait(false))
                .SelectMany(result => result.Errors as IEnumerable<ValidationFailure>)
                .Where(failure => failure != null).ToList();

            if (failures.Any())
            {
                throw new ValidationException($"Validation failed: {string.Join(", ", failures)}");
            }
        }
    }
}