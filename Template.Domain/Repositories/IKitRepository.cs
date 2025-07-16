using Template.Domain.Entities.Materials;

namespace Template.Domain.Repositories;

public interface IKitRepository : IGenericRepository<Kit>
{
    Task<List<Kit>> GetFilteredKit(int pageNum, int pageSize, string? brandName, bool? hasImplants, bool? isMainKit = false);
}
