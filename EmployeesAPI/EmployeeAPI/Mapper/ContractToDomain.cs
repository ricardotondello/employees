namespace EmployeeAPI.Mapper
{
    public static class ContractToDomain
    {
        public static Employee.Domain.Region ToDomain(this EmployeeAPI.Contracts.Input.Region region)
        {
            return region == null
                ? null
                : Employee.Domain.Region.Create(region.Id, region.Name, region.ToDomain());
        }
    }
}
