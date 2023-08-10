using ChoETL;
using Employees.Entities;

namespace Employee.Infrastructure.DataBase.Initializer;

public class DbInitializer
{
    private readonly DataBaseCtx _context;

    public DbInitializer(DataBaseCtx context)
    {
        _context = context;
    }
    public void Initialize()
    {
        if (!_context.Regions.Any())
        {
            var regions = ReadRegionCsv().ToList();

            _context.Regions.AddRange(regions);
            _context.SaveChanges();
        }

        if (_context.Employees.Any()) return;

        var employees = ReadEmployeesCsv().ToList();
        _context.Employees.AddRange(employees);
        _context.SaveChanges();
    }

    private static IEnumerable<Employees.Entities.Employee> ReadEmployeesCsv()
    {
        const string filename = @"employees.csv";
        if (!File.Exists(filename))
        {
            return Enumerable.Empty<Employees.Entities.Employee>();
        }

        return new ChoCSVReader(filename)
            .WithFirstLineHeader()
            .Select(e => new Employees.Entities.Employee(Guid.NewGuid(), e.Name, e.Surname, int.Parse(e.RegionId)))
            .ToList();
    }

    private static IEnumerable<Region> ReadRegionCsv()
    {
        const string filename = @"regions.csv";
        if (!File.Exists(filename))
        {
            return Enumerable.Empty<Region>();
        }

        return new ChoCSVReader(filename)
            .WithFirstLineHeader()
            .Select(s => new Region(int.Parse(s.Id), s.Name, int.TryParse(s.ParentId, out int parentId) ? parentId : (int?)null))
            .ToList();
    }
}