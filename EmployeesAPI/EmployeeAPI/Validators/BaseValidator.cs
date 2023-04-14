using FluentValidation;
using FluentValidation.Results;

namespace EmployeeAPI.Validators
{
    internal class BaseValidator<T> : AbstractValidator<T>
    {
        private static readonly ValidationResult ValidationResult = new(new[]
        {
            new ValidationFailure(typeof(T).ToString(),
                "Parameter could not be validated. Make sure it has the correct format")
        });

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return context.InstanceToValidate == null ? ValidationResult : base.Validate(context);
        }
    }
}