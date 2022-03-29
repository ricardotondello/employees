
using EmployeesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Infrastructure.DataBase
{
    public class DataBaseCtx : DbContext
    {
        public DbSet<EmployeesAPI.Entities.Region> Regions { get; set; }
        public DbSet<EmployeesAPI.Entities.Employee> Employees { get; set; }

        public DataBaseCtx(DbContextOptions<DataBaseCtx> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Region>().HasOne(tr => tr.Parent)
            //     .WithOne()
            //     //.HasForeignKey<Region>(tr => tr.ParentId)
            //     .IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmployeesAPI.Entities.Employee>().ToTable("Employee");
        }
    }
}
