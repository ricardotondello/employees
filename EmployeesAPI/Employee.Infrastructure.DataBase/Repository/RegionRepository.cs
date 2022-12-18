using System.Data;
using Employee.Domain;
using Employee.Toolkit;
using Employee.Application.Interfaces.Repositories;
using Employee.Infrastructure.DataBase.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.DataBase.Repository;

public class RegionRepository : IRegionRepository
{
    private readonly DataBaseCtx _context;

    public RegionRepository(DataBaseCtx context)
    {
        _context = context;
    }

    public async Task<Option<Region>> CreateRegionAsync(Region region)
    {
        var hasValue = await _context.Regions.AnyAsync(s => s.Id == region.Id);
        var entity = region.ToEntity();
        var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        if (hasValue)
        {
            _context.Regions.Update(entity);
        }
        else
        {
            _context.Regions.Add(entity);
        }

        var updates = await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return updates > 0
            ? Option<Region>.Some(region)
            : Option<Region>.None;
    }

    public async Task<Option<Region>> GetByIdAsync(int id)
    {
        var region = await _context.Regions.Include(i => i.Parent).SingleOrDefaultAsync(s => s.Id == id);
        return region != null
            ? Option<Region>.Some(region.ToDomain())
            : Option<Region>.None;
    }

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        var regions = await _context.Regions.Include(i => i.Parent).ToListAsync();
        return regions.ToDomain();
    }
}