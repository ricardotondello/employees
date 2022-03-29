using Employee.Toolkit;

namespace EmployeeAPI.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Option<Employee.Domain.Employee>> CreateEmployeeAsync(Employee.Domain.Employee employee);
        Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id);

        Task<Option<Employee.Domain.Employee>> GetEmployeeByRegionAsync(int regionId);
    }
}
