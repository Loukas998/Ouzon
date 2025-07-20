using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ImplantRepository : GenericRepository<Implant>, IImplantRepository
{
    public ImplantRepository(TemplateDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Implant>> GetFilteredImplants(string? brand, float? radius, float? width, float? height, float? kitId, int? pageNum, int? pageSize)
    {
        var implants = dbContext.Implants.AsQueryable();

        if (!string.IsNullOrEmpty(brand))
        {
            implants.Where(i => i.Brand!.Contains(brand));
        }
        if (radius != null)
        {
            implants.Where(i => i.Radius == radius);
        }
        if (width != null)
        {
            implants.Where(i => i.Width == width);
        }
        if (height != null)
        {
            implants.Where(i => i.Height == height);
        }
        if (kitId != null)
        {
            implants.Where(i => i.KitId == kitId);
        }

        if (pageNum != null && pageSize != null)
        {
            var pagedResult = await implants.Skip((int)((pageNum - 1) * pageSize))
                .Take((int)pageSize)
                .ToListAsync();
            return pagedResult;

        }

        var result = await implants.ToListAsync();
        return result;

    }
}
