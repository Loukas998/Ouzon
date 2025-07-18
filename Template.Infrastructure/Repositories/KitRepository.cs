using Microsoft.EntityFrameworkCore;
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
            .Include(c=>c.Implants)
            .AsQueryable();
        if(!string.IsNullOrWhiteSpace(brandName))
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

}
