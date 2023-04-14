using Employee.Contracts.Output;

namespace EmployeeAPI.Mapper
{
    public static class DomainToContract
    {
        public static Region ToContract(this Employee.Domain.Region region) =>
            region == null ? null : Region.Create(region.Id, region.Name);

        public static IEnumerable<Region> ToContract(this IEnumerable<Employee.Domain.Region> regions) =>
            regions.Select(s => s.ToContract());

        public static Employee.Contracts.Output.Employee ToContract(this Employee.Domain.Employee employee) =>
            employee == null
                ? null
                : Employee.Contracts.Output.Employee.Create(employee.Id, employee.Name, employee.Surname,
                    employee.Region.ToContract());

        public static IEnumerable<Employee.Contracts.Output.Employee> ToContract(
            this IEnumerable<Employee.Domain.Employee> employees) =>
            employees.Select(s => s.ToContract());

        public static EmployeeAggregate ToAggregateContract(this Employee.Domain.Employee employee)
        {
            if (employee == null)
                return null;

            if (employee.Region == null)
            {
                return EmployeeAggregate.Create($"{employee.Name} {employee.Surname}", null);
            }

            var regions = new List<Region> { employee.Region.ToContract() };

            if (employee.Region.Parent != null) //TODO: make it recursive
            {
                regions.Add(employee.Region.Parent.ToContract());
            }

            return EmployeeAggregate.Create($"{employee.Name} {employee.Surname}", regions);
        }
    }
}