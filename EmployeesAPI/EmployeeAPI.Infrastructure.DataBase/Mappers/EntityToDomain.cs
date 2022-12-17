using Employee.Domain;

namespace EmployeeAPI.Infrastructure.DataBase.Mappers
{
    public static class EntityToDomain
    {
        public static Employee.Domain.Region ToDomain(this EmployeesAPI.Entities.Region? region) =>
            (region == null
                ? null
                : Region.Create(region.Id, region.Name,
                    region is {ParentId: { }, Parent: { }}
                        ? Region.Create(region.Parent.Id, region.Parent.Name, null)
                        : null))!;

        public static Employee.Domain.Employee ToDomain(this EmployeesAPI.Entities.Employee? employee) =>
            (employee == null
                ? null
                : Employee.Domain.Employee.Create(employee.Id, employee.Name, employee.Surname,
                    employee.Region.ToDomain()))!;
    }
}