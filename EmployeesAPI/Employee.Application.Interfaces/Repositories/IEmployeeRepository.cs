using Employee.Toolkit;

namespace Employee.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<Option<Employee.Domain.Employee>> CreateEmployeeAsync(Employee.Domain.Employee employee);
    Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id);
    Task<IEnumerable<Employee.Domain.Employee>> GetEmployeesByRegionAsync(int regionId);
    Task<IEnumerable<Employee.Domain.Employee>> GetAllAsync();
}