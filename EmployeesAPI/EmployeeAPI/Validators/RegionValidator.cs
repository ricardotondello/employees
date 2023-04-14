using Employee.Contracts.Input;
using FluentValidation;

namespace EmployeeAPI.Validators
{
    public class RegionValidator : AbstractValidator<Region>
    {
        public RegionValidator()
        {
            RuleFor(r => r.Id).SetValidator(new IdValidator("Id"));
            RuleFor(r => r.Name).StringValidator("Name");
        }
    }
}