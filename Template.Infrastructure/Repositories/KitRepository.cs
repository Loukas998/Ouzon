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
	
}
