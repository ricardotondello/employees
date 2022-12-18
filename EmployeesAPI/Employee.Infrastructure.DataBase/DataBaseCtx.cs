using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.DataBase;

public class DataBaseCtx : DbContext
{
    public DbSet<Employees.Entities.Region> Regions { get; set; }
    public DbSet<Employees.Entities.Employee> Employees { get; set; }

    public DataBaseCtx(DbContextOptions<DataBaseCtx> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}