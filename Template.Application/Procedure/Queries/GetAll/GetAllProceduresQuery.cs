using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQuery(DateTime? from, DateTime? to, int? minNumberOfAssistants, int? maxNumberOfAssistants, string? doctorName, List<string> assistantNames, string? clinicName, string? clinicAddress) : IQuery<IEnumerable<ProcedureDto>>
    {
        public DateTime? From { get; set; } = from;
        public DateTime? To { get; set; } = to;
        public int? MinNumberOfAssistants { get; set; } = minNumberOfAssistants;
        public int? MaxNumberOfAssistants { get; set; } = maxNumberOfAssistants;
        public string? DoctorName { get; set; } = doctorName;
        public List<string> AssistantNames { get; set; } = assistantNames;
        public string? ClinicName { get; set; } = clinicName;
        public string? ClinicAddress { get; set; } = clinicAddress;
    }
}
