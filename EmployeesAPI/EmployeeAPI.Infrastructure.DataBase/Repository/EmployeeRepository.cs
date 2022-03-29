using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;

namespace EmployeeAPI.Infrastructure.DataBase.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Option<Employee.Domain.Employee>> CreateEmployeeAsync(Employee.Domain.Employee employee)
        {
            return Task.FromResult(Option<Employee.Domain.Employee>.Some(employee));
        }
    }
}
