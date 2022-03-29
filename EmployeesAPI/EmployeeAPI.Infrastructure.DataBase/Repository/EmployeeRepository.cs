using Employee.Domain;
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

        public Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id)
        {
            return Task.FromResult(
                Option<Employee.Domain.Employee>.Some(Employee.Domain.Employee.Create(id, "test", "tt", null)));
        }

        public Task<Option<Employee.Domain.Employee>> GetEmployeeByRegionAsync(int regionId)
        {
            var region = Employee.Domain.Region.Create(regionId, $"Name {regionId}", Region.Create(0, "Name 0", null));

            var emp = Employee.Domain.Employee.Create(Guid.NewGuid(), "emp name", "hhh", region);
            return Task.FromResult(Option<Employee.Domain.Employee>.Some(emp));
        }
    }
}
