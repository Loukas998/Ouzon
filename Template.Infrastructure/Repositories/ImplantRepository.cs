using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ImplantRepository : GenericRepository<Implant>, IImplantRepository
{
	public ImplantRepository(TemplateDbContext dbContext) : base(dbContext)
	{

	}
}
