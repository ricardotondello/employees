using ChoETL;
using EmployeesAPI.Entities;

namespace EmployeeAPI.Infrastructure.DataBase.Initializer
{
    public class DbInitializer
    {
        public static void Initialize(DataBaseCtx context)
        {
            context.Database.EnsureCreated();

            if (!context.Regions.Any())
            {
                var regions = ReadRegionCsv().ToList();

                context.Regions.AddRange(regions);
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                var employees = ReadEmployeesCsv();
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }

        private static IEnumerable<EmployeesAPI.Entities.Employee> ReadEmployeesCsv()
        {
            var filename = @"employees.csv";
            if (!File.Exists(filename))
            {
                return Enumerable.Empty<EmployeesAPI.Entities.Employee>();
            }
            var employees = new List<EmployeesAPI.Entities.Employee>();
            using var r = new ChoCSVLiteReader();
            var recNum = r.ReadFile(filename).GetEnumerator();
            while (recNum.MoveNext())
            {
                var values = recNum.Current;
                var regionId = int.Parse(values[0]);
                var employee = new EmployeesAPI.Entities.Employee(values[1], values[2], regionId);
                employees.Add(employee);
            }

            return employees;
        }

        private static IEnumerable<Region> ReadRegionCsv()
        {
            var filename = @"regions.csv";
            if (!File.Exists(filename))
            {
                return Enumerable.Empty<Region>();
            }
            var regions = new List<Region>();
            using var r = new ChoCSVLiteReader();
            var recNum = r.ReadFile(filename).GetEnumerator();
            while (recNum.MoveNext())
            {
                var values = recNum.Current;

                var parentId = values.Length > 1
                    ? int.TryParse(values[2], out var value) ? value : (int?)null
                    : (int?)null;
                var region = new Region(int.Parse(values[1]), values[0], parentId);
                regions.Add(region);
            }

            return regions;
        }
    }
}
