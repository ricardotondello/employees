using Employee.Domain;
using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;

namespace EmployeeAPI.Infrastructure.DataBase.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public Task<Option<Region>> CreateRegionAsync(Region region)
        {
            return Task.FromResult(Option<Region>.Some(region));
        }

        public Task<Option<Region>> GetByIdAsync(int id)
        {
            return Task.FromResult(Option<Region>.Some( Region.Create(1, "gggg", null)));
        }
    }
}
