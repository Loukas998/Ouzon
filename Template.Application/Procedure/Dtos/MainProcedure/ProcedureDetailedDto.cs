using Template.Application.Kits.Dtos;
using Template.Application.Tools.Dtos;
using Template.Application.Users.Dtos;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Dtos.MainProcedure
{
    public class ProcedureDetailedDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        //public List<string>? AssistantIds { get; set; }
        public int CategoryId { get; set; }
        public EnumProcedureStatus Status { get; set; } = EnumProcedureStatus.REQUEST_SENT;
        public DateTime Date { get; set; }
        public UserDto Doctor { get; set; }
        public List<KitDto>? SurgicalKits { get; set; } = []; // surgical kits
        public List<ToolDto>? RequiredTools { get; set; } = []; // required tools
        public List<ProcedureImplantToolsDetailsDto>? ImplantKits { get; set; } = []; // implant kits
                                                                                      // public IEnumerable<KitDto>? KitsWithoutImplants { get; set; } = []; // required tools
        public List<UserDto>? Assistants { get; set; } = [];
        // public IEnumerable<ImplantDto>? ProcedureImplantsWithoutTools { get; set; } = []; // implant kits
        // public IEnumerable<ProcedureImplantToolsDetailsDto>? ProcedureImplantsWithTools { get; set; } = []; // implant kits

    }
}
