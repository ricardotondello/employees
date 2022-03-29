using Employee.Domain;

namespace EmployeeAPI.Infrastructure.DataBase.Mappers
{
    public static class EntityToDomain
    {
        public static Region ToDomain(this EmployeesAPI.Entities.Region region) =>
            region == null
                ? null
                : Region.Create(region.Id, region.Name, region.ParentId != null ? Region.Create(region.Parent.Id, region.Parent.Name, null) : null);
    }
}
