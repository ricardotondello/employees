using FluentValidation;

namespace EmployeeAPI.Validators
{
    internal class IdValidator : BaseValidator<int>
    {
        private readonly string _parameterName = "Id";

        public IdValidator(string parameterName = null)
        {
            if (!string.IsNullOrEmpty(parameterName))
                _parameterName = parameterName;

            CascadeMode = CascadeMode.Stop;

            RuleFor(id => id).GreaterThan(0).WithMessage($"{_parameterName} is invalid");
        }
    }
}
