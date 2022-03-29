using Employee.Domain;
using Employee.Toolkit;

namespace EmployeeAPI.Application.Interfaces.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateRegionAsync(Region region);
        Task<Option<Region>> GetByIdAsync(int id);
    }
}
