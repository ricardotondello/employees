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

            if (context.Employees.Any()) return;
            
            var employees = ReadEmployeesCsv().ToList();
            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private static IEnumerable<EmployeesAPI.Entities.Employee> ReadEmployeesCsv()
        {
            const string filename = @"employees.csv";
            if (!File.Exists(filename))
            {
                return Enumerable.Empty<EmployeesAPI.Entities.Employee>();
            }

            var employees = new List<EmployeesAPI.Entities.Employee>();
            using var r = new ChoCSVLiteReader();
            using var recNum = r.ReadFile(filename).GetEnumerator();
            while (recNum.MoveNext())
            {
                var values = recNum.Current;
                var regionId = int.Parse(values?[0]!);
                var name = values?[1]!;
                var surname = values?[2]!;
                var employee = new EmployeesAPI.Entities.Employee(name, surname, regionId);
                employees.Add(employee);
            }

            return employees;
        }

        private static IEnumerable<Region> ReadRegionCsv()
        {
            const string filename = @"regions.csv";
            if (!File.Exists(filename))
            {
                return Enumerable.Empty<Region>();
            }

            var regions = new List<Region>();
            using var r = new ChoCSVLiteReader();
            using var recNum = r.ReadFile(filename).GetEnumerator();
            while (recNum.MoveNext())
            {
                var values = recNum.Current;

                var parentId = values!.Length > 1
                    ? int.TryParse(values[2], out var value) ? value : null
                    : (int?) null;
                var id = int.Parse(values[1]);
                var name = values[0];
                var region = new Region(id, name, parentId);
                regions.Add(region);
            }

            return regions;
        }
    }
}