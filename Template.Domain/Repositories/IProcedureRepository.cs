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
        Task<int> AddProcedureAssistant(ProcedureAssistant entity);
        Task<Procedure> GetDetailedWithId(int id);
        Task<List<Procedure>> GetFilteredProcedures(int pageSize, int pageNum, string? DoctorId, string? AssistantId);
        Task<Procedure> GetProcedureWithAssistants(int Id);
        Task<Procedure> GetProcedureWithKits(int Id);
        Task<Procedure> GetProcedureWithToolsNotInKit(int Id);
    }
}
