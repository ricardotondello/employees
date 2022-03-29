using Employee.Domain;
using Employee.Toolkit;

namespace EmployeeAPI.Application.Interfaces.Services
{
    public interface IRegionService
    {
        Task<Region> AddAsync(Region region);
        Task<Option<Region>> GetByIdAsync(int id);

    }
}
