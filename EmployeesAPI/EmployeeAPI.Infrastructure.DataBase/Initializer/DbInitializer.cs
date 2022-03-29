using ChoETL;
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
            
            var regions = ReadRegionCSV().ToList();

            context.Regions.AddRange(regions);
            context.SaveChanges();
        }

        private static IEnumerable<Region> ReadRegionCSV()
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
