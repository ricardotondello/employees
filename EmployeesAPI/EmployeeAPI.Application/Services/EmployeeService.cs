using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;
using EmployeeAPI.Application.Interfaces.Services;

namespace EmployeeAPI.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<Option<Employee.Domain.Employee>> AddAsync(Employee.Domain.Employee employee)
        {
            return _employeeRepository.CreateEmployeeAsync(employee);
        }
    }
}
