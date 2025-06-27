using Template.Domain.Entities.Schedule;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
{
    public HolidayRepository(TemplateDbContext dbContext) : base(dbContext)
    {

    }
}
