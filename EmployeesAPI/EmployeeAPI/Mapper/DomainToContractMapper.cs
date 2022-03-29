using EmployeeAPI.Contracts.Output;

namespace EmployeeAPI.Mapper
{
    public static class DomainToContractMapper
    {
        public static Region ToContract(this Employee.Domain.Region region) =>
            region == null
                ? null
                : Region.Create(region.Id, region.Name);
    }
}
