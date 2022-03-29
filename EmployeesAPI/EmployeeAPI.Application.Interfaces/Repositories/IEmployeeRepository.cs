using Employee.Toolkit;

namespace EmployeeAPI.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Option<Employee.Domain.Employee>> CreateEmployeeAsync(Employee.Domain.Employee employee);
    }
}
