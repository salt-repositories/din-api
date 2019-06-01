using System.Linq;
using FluentAssertions;
using FluentValidation.Results;

namespace Din.Domain.Tests.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void ShouldContainErrorForProperty(this ValidationResult validationResult, string propertyName)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.First().PropertyName.Should().Contain(propertyName);
        }
    }
}
