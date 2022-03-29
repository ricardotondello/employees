namespace EmployeeAPI.Mapper
{
    public static class DomainToContractMapper
    {
        public static Contracts.Output.Region ToContract(this Employee.Domain.Region region) =>
            region == null
                ? null
                : Contracts.Output.Region.Create(region.Id, region.Name);

        public static Contracts.Output.Employee ToContract(this Employee.Domain.Employee employee) =>
            employee == null
                ? null
                : Contracts.Output.Employee.Create(employee.Id, employee.Name, employee.Region.ToContract());
    }
}
