using FluentValidation;

namespace EmployeeAPI.Validators
{
    public class EmployeeValidator : AbstractValidator<Contracts.Input.Employee>
    {
        public EmployeeValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(r => r.Surname).StringValidator("Surname");
            RuleFor(r => r.Name).StringValidator("Name");
            RuleFor(r => r.RegionId).SetValidator(new IdValidator());
        }
    }
}
