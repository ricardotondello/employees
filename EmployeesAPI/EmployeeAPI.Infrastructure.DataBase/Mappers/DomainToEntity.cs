namespace EmployeeAPI.Infrastructure.DataBase.Mappers
{
    public static class DomainToEntity
    {
        public static EmployeesAPI.Entities.Employee ToEntity(this Employee.Domain.Employee domain) =>
            domain == null
                ? null
                : new EmployeesAPI.Entities.Employee(domain.Name, domain.Surname, domain.Region.Id);

        public static EmployeesAPI.Entities.Region ToEntity(this Employee.Domain.Region region) =>
            region == null
                ? null
                : new EmployeesAPI.Entities.Region(region.Id, region.Name, region.Parent?.Id);
    }
}
