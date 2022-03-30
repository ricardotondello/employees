using EmployeeAPI.Contracts.Input;
using FluentValidation;

namespace EmployeeAPI.Validators
{
    public class RegionValidator : AbstractValidator<Region>
    {
        public RegionValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(r => r.Id).SetValidator(new IdValidator());
            RuleFor(r => r.Name).StringValidator("Name");
        }
    }
}
