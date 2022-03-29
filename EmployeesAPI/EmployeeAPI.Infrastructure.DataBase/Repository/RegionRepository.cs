using Employee.Domain;
using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;
using EmployeeAPI.Infrastructure.DataBase.Mappers;

namespace EmployeeAPI.Infrastructure.DataBase.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DataBaseCtx _context;

        public RegionRepository(DataBaseCtx context)
        {
            _context = context;
        }

        public Task<Option<Region>> CreateRegionAsync(Region region)
        {
            return Task.FromResult(Option<Region>.Some(region));
        }

        public Task<Option<Region>> GetByIdAsync(int id)
        
        {
            var region = _context.Regions.SingleOrDefault(s => s.Id == id);
            if (region != null)
            {
                return Task.FromResult(Option<Region>.Some(region.ToDomain()));
            }
            return Task.FromResult(Option<Region>.None);
     
        }
    }
}
