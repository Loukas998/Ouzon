using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQuery : IQuery<IEnumerable<ProcedureDto>>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? MinNumberOfAssistants { get; set; }
        public int? MaxNumberOfAssistants { get; set; }
        public string? DoctorName { get; set; }
        public List<string>? AssistantNames { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicAddress { get; set; }
        public EnumProcedureStatus? Status { get; set; }  // ✅ Nullable

        public GetAllProceduresQuery(
            DateTime? from,
            DateTime? to,
            int? minNumberOfAssistants,
            int? maxNumberOfAssistants,
            string? doctorName,
            List<string>? assistantNames,
            string? clinicName,
            string? clinicAddress,
            EnumProcedureStatus? status // ✅ Nullable
        )
        {
            From = from;
            To = to;
            MinNumberOfAssistants = minNumberOfAssistants;
            MaxNumberOfAssistants = maxNumberOfAssistants;
            DoctorName = doctorName;
            AssistantNames = assistantNames;
            ClinicName = clinicName;
            ClinicAddress = clinicAddress;
            Status = status;
        }
    }


}
