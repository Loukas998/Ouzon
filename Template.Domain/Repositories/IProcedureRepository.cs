using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Enums;

namespace Template.Domain.Repositories
{
    public interface IProcedureRepository : IGenericRepository<Procedure>
    {
        Task<int> AddProcedureAssistant(ProcedureAssistant entity);

        Task<List<Procedure>> GetAllFilteredProcedures(string? DoctorId, string? AssistantId, DateTime? from, DateTime? to, int? minNumberOfAssistants,
            int? maxNumberOfAssistants, string? doctorName, List<string>? assistantNames,
            string? clinicName, string? clinicAddress, EnumProcedureStatus? status, string isDoctorAuthenticated, string isAssistantAuthenticated);
        Task<Procedure> GetDetailedWithId(int id);
        Task<List<Procedure>> GetPagedFilteredProcedures(int pageSize, int pageNum, string? DoctorId, string? AssistantId);
        Task<Procedure> GetProcedureWithAssistants(int Id);
        Task<Procedure> GetProcedureWithKits(int Id);
        Task<Procedure> GetProcedureWithToolsNotInKit(int Id);
    }
}
