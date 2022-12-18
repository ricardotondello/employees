using Employee.Toolkit;
using Employee.Application.Interfaces.Repositories;
using Employee.Application.Interfaces.Services;

namespace Employee.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Option<Employee.Domain.Employee>> AddAsync(Employee.Domain.Employee employee) =>
            _employeeRepository.CreateEmployeeAsync(employee);

        public Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id) => _employeeRepository.GetByIdAsync(id);

        public Task<IEnumerable<Employee.Domain.Employee>> GetEmployeesByRegionAsync(int regionId) =>
            _employeeRepository.GetEmployeesByRegionAsync(regionId);

        public Task<IEnumerable<Employee.Domain.Employee>> GetAllAsync()
            => _employeeRepository.GetAllAsync();
    }
}