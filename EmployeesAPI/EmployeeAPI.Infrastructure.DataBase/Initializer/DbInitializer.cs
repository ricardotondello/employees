using EmployeesAPI.Entities;

namespace EmployeeAPI.Infrastructure.DataBase.Initializer
{
    public class DbInitializer
    {
        public static void Initialize(DataBaseCtx context)
        {
            context.Database.EnsureCreated();

            if (context.Regions.Any())
            {
                return; // DB has been seeded
            }

            var regions = new List<Region>()
            {
                new Region(1, "one"),
                new Region(2, "two")
            };
            
            context.Regions.AddRange(regions);
            context.SaveChanges();
            
        }
    }
}
