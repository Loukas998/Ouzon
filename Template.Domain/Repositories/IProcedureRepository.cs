using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Repositories
{
    public interface IProcedureRepository : IGenericRepository<Procedure>
    {
        Task<List<int>> AddKitsToProcedure(int procedureId, List<int> kitsIds);
        Task<List<int>> AddToolsToProcedure(int procedureId, List<int> toolIds);
    }
}
