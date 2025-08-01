using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ToolRepository : GenericRepository<Tool>, IToolRepository
{
    public ToolRepository(TemplateDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Tool>> GetFilteredTools(string? name, float? width, float? height, float? thickness, int? kitId, int? categoryId, int? pageNum, int? pageSize)
    {
        var tools = dbContext.Tools.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            tools.Where(t => t.Name.Contains(name));
        }
        if (width != null)
        {
            tools.Where(t => t.Width == width);
        }
        if (height != null)
        {
            tools.Where(t => t.Height == height);
        }
        if (thickness != null)
        {
            tools.Where(t => t.Thickness == thickness);
        }
        if (kitId != null)
        {
            tools.Where(t => t.KitId == kitId);
        }
        if (categoryId != null)
        {
            tools.Where(t => t.CategoryId == categoryId);
        }


        if (pageNum != null && pageSize != null)
        {
            var pagedResult = await tools.Skip((int)((pageNum - 1) * pageSize))
                .Take((int)pageSize)
                .ToListAsync();
            return pagedResult;

        }

        var result = await tools.ToListAsync();
        return result;
    }
}
