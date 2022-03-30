using System.Data;
using Employee.Toolkit;
using EmployeeAPI.Application.Interfaces.Repositories;
using EmployeeAPI.Infrastructure.DataBase.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Infrastructure.DataBase.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataBaseCtx _context;

        public EmployeeRepository(DataBaseCtx context)
        {
            _context = context;
        }

        public async Task<Option<Employee.Domain.Employee>> CreateEmployeeAsync(Employee.Domain.Employee employee)
        {
            var hasEmployee = await _context.Employees.Include(i => i.Region).AnyAsync(s => s.Id == employee.Id);
            var entity = employee.ToEntity();
            var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            if (hasEmployee)
            {
                _context.Employees.Update(entity);
            }
            else
            {
                _context.Employees.Add(entity);
            }

            var updates = await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return updates > 0
                ? Option<Employee.Domain.Employee>.Some(employee)
                : Option<Employee.Domain.Employee>.None;
        }

        public async Task<Option<Employee.Domain.Employee>> GetByIdAsync(Guid id)
        {
            var employee = await _context.Employees.Include(i => i.Region).FirstOrDefaultAsync(s => s.Id == id);
            if (employee != null)
            {
                return Option<Employee.Domain.Employee>.Some(employee.ToDomain());
            }

            return Option<Employee.Domain.Employee>.None;
        }

        public async Task<IEnumerable<Employee.Domain.Employee>> GetEmployeesByRegionAsync(int regionId)
        {
            var region = await _context.Regions
                .SingleOrDefaultAsync(s => s.Id == regionId);


            if (region == null)
            {
                return Enumerable.Empty<Employee.Domain.Employee>();
            }

            var employees = _context.Employees
                .FromSqlRaw("SELECT * FROM Employee e WITH(NOLOCK) WHERE e.RegionId in(" +
                            "SELECT DISTINCT COALESCE(r3.Id, r2.Id , r.Id) AS Id " +
                            "FROM Region r WITH(NOLOCK) LEFT JOIN " +
                            "Region r2 WITH(NOLOCK) ON r.Id = r2.ParentId " +
                            "LEFT JOIN Region r3 WITH(NOLOCK) ON r2.Id = r3.ParentId " +
                            $"WHERE (R.Id = {region.Id} OR r2.Id = {region.Id} OR r3.Id = {region.Id}))")
                .Include(i => i.Region)
                .Include(i => i.Region.Parent)
                .Include(i => i.Region.Parent.Parent);
            if (employees.Any())
            {
                var employeesEnumerable = employees.Select(s => s.ToDomain()).AsEnumerable();

                return employeesEnumerable;
            }

            return Enumerable.Empty<Employee.Domain.Employee>();
        }
    }
}