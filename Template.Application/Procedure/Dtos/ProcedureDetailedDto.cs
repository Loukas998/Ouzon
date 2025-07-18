using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Implants.Dtos;
using Template.Application.Kits.Dtos;
using Template.Application.Tools.Dtos;
using Template.Application.Users.Dtos;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Dtos
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
        public IEnumerable<KitDto>? MainKits { get; set; } = []; // surgical kits
        public IEnumerable<ToolDto>? ToolsNotInKit { get; set; } = []; // required tools
        public IEnumerable<KitDto>? KitsWithImplants { get; set; } = []; // implant kits
        public IEnumerable<KitDto>? KitsWithoutImplants { get; set; } = []; // required tools
        public IEnumerable<UserDto>? Assistants { get; set; } = [];
        public IEnumerable<ImplantDto>? ProcedureImplantsWithoutTools { get; set; } = []; // implant kits
        public IEnumerable<ProcedureImplantToolsDetailsDto>? ProcedureImplantsWithTools { get; set; } = []; // implant kits

    }
}
