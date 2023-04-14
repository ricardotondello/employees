using FluentValidation;

namespace EmployeeAPI.Validators
{
    internal class IdValidator : BaseValidator<int>
    {
        public IdValidator(string parameterName)
        {
            RuleFor(id => id)
                .GreaterThan(0)
                .WithMessage($"{parameterName} is invalid");
        }
    }
}