using Template.Domain.Entities.Materials;

namespace Template.Domain.Repositories;

public interface IImplantRepository : IGenericRepository<Implant>
{
    public Task<List<Implant>> GetFilteredImplants(string? brand, float? radius, float? width, float? height, float? kitId, int? pageNum, int? pageSize);
}
