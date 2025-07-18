using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Schedule;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
{
    public HolidayRepository(TemplateDbContext dbContext) : base(dbContext)
    {
        

    }
    public async Task<List<Holiday>>GetHolidaysWithFilter(int pageNum, int pageSize, DateTime? FromDate, DateTime? ToDate, string? AssistantId)
    {
        var query = dbContext.Holidays.AsQueryable();
        if (FromDate != null)
        {
            query = query.Where(h => h.From > FromDate);
        }
        if (ToDate != null)
        {
            query = query.Where(h => h.To > ToDate);
        }
        if(AssistantId != null)
        {
            query = query.Where(h => h.UserId == AssistantId);
        }
        return await query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
