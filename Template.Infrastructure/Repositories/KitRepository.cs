using Microsoft.EntityFrameworkCore;
using Template.Application.Kits.Dtos;
using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class KitRepository : GenericRepository<Kit>, IKitRepository
{

    public KitRepository(TemplateDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<Kit>> GetFilteredKit(int pageNum, int pageSize, string? brandName, bool? hasImplants, bool? isMainKit = false)
    {
        var query = dbContext.Kits
            .Include(c => c.Implants)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(brandName))
        {
            query = query.Where(c => c.Name.Contains(brandName!));
        }
        if (isMainKit != null)
        {
            query = query.Where(c => c.IsMainKit == isMainKit);
        }
        if (hasImplants != null)
        {
            query = query.Where(c => c.Implants.Any() == (bool)hasImplants);

        }

        var result = await query.Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }
    public async Task<List<(int Id, string? Name, bool IsMainKit, int ImplantCount, int ToolCount, string ImagePath)>> GetKitsWithToolsAndImplantsCount()
    {
        var kits = await dbContext.Kits.Select(k => new KitDto()
        {
            Id = k.Id,
            Name = k.Name,
            IsMainKit = k.IsMainKit,
            ImplantCount = k.Implants.Count(),
            ToolCount = k.Tools.Count(),
            ImagePath = k.ImagePath
        })
            .ToListAsync();

        return kits.Select(x => (x.Id, x.Name, x.IsMainKit, x.ImplantCount, x.ToolCount, x.ImagePath))
            .ToList(); ;
    }
    public async Task<Kit> GetKitDetails(int Id)
    {
        return await dbContext.Kits
            .Include(k => k.Tools)
            .Include(k => k.Implants)
            .FirstOrDefaultAsync(k => k.Id == Id);
    }

}
