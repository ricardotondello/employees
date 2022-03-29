using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Domain;
using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;
using EmployeeAPI.Application.Interfaces.Services;

namespace EmployeeAPI.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        
        public Task<Region> AddAsync(Region region)
        {
            return _regionRepository.CreateRegionAsync(region);
        }

        public Task<Option<Region>> GetByIdAsync(int id)
        {
            return _regionRepository.GetByIdAsync(id);
        }
    }
}
