using EmployeeAPI.Contracts.Output;

namespace EmployeeAPI.Mapper
{
    public static class DomainToContract
    {
        public static Contracts.Output.Region ToContract(this Employee.Domain.Region region) =>
            region == null
                ? null
                : Contracts.Output.Region.Create(region.Id, region.Name);

        public static Contracts.Output.Employee ToContract(this Employee.Domain.Employee employee) =>
            employee == null
                ? null
                : Contracts.Output.Employee.Create(employee.Id, employee.Name, employee.Surname,
                    employee.Region.ToContract());

        public static Contracts.Output.EmployeeAggregate ToAggregateContract(this Employee.Domain.Employee employee)
        {
            if (employee == null)
                return null;

            if (employee.Region == null)
            {
                return Contracts.Output.EmployeeAggregate.Create($"{employee.Name} {employee.Surname}", null);
            }

            var regions = new List<Region>();
            regions.Add(employee.Region.ToContract());

            if (employee.Region.Parent != null) //TODO: make it recursive
            {
                regions.Add(employee.Region.Parent.ToContract());
            }

            return Contracts.Output.EmployeeAggregate.Create($"{employee.Name} {employee.Surname}",
                regions);
        }
    }
}