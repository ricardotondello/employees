using FluentValidation;

namespace EmployeeAPI.Validators
{
    internal class IdValidator : BaseValidator<int>
    {
        public IdValidator(string parameterName)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(id => id)
                .GreaterThan(0)
                .WithMessage($"{parameterName} is invalid");
        }
    }
}