using Employee.Domain;

namespace Employee.Infrastructure.DataBase.Mappers;

public static class DomainToEntity
{
    public static Employees.Entities.Employee ToEntity(this Employee.Domain.Employee domain) =>
        domain == null
            ? null
            : new Employees.Entities.Employee(domain.Name, domain.Surname, domain.Region.Id);

    public static Employees.Entities.Region ToEntity(this Region region) =>
        region == null
            ? null
            : new Employees.Entities.Region(region.Id, region.Name, region.Parent?.Id);
}