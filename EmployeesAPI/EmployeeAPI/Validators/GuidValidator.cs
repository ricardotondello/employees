using FluentValidation;

namespace EmployeeAPI.Validators
{
    internal class GuidValidator : AbstractValidator<Guid>
    {
        private readonly string _parameterName = "Id";
        public GuidValidator(string parameterName = null)
        {
            if (!string.IsNullOrEmpty(parameterName))
                _parameterName = parameterName;

            CascadeMode = CascadeMode.Stop;

            RuleFor(id => id).NotEmpty().WithMessage($"{_parameterName} is invalid");
        }
    }
}
