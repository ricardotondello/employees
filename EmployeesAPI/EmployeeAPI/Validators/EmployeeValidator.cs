using FluentValidation;

namespace EmployeeAPI.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee.Contracts.Input.Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(r => r.Surname).StringValidator("Surname");
            RuleFor(r => r.Name).StringValidator("Name");
            RuleFor(r => r.RegionId).SetValidator(new IdValidator("RegionId"));
        }
    }
}