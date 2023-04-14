using Employee.Domain;
using Employee.Toolkit;
using Employee.Application.Interfaces.Repositories;
using Employee.Application.Interfaces.Services;

namespace Employee.Application.Services;

public class RegionService : IRegionService
{
    private readonly IRegionRepository _regionRepository;

    public RegionService(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public Task<Option<Region>> AddAsync(Region region) => _regionRepository.CreateRegionAsync(region);

    public Task<Option<Region>> GetByIdAsync(int id) => _regionRepository.GetByIdAsync(id);

    public Task<IEnumerable<Region>> GetAllAsync() => _regionRepository.GetAllAsync();
}