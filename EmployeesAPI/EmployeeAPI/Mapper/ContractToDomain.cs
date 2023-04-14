using Employee.Domain;

namespace EmployeeAPI.Mapper
{
    public static class ContractToDomain
    {
        public static Region ToDomain(this Employee.Contracts.Input.Region region) =>
            region == null
                ? null
                : Employee.Domain.Region.Create(region.Id, region.Name,
                    region.RegionId > 0 ? Region.Create((int)region.RegionId) : null);

        public static Employee.Domain.Employee ToDomain(this Employee.Contracts.Input.Employee employee) =>
            employee == null
                ? null
                : Employee.Domain.Employee.Create(Guid.NewGuid(), employee.Name, employee.Surname,
                    Region.Create(employee.RegionId));
    }
}