using Employee.Toolkit;

namespace EmployeeAPI.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Option<Employee.Domain.Employee>> AddAsync(Employee.Domain.Employee employee);
        Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id);
    }
}
