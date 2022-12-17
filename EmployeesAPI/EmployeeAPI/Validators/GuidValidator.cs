using FluentValidation;

namespace EmployeeAPI.Validators
{
    internal class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator(string parameterName)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(id => id)
                .NotEmpty()
                .Must(g => g != Guid.Empty)
                .WithMessage($"{parameterName} is invalid");
        }
    }
}