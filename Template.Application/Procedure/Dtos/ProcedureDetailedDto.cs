using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IEnumerable<ToolDto>? ToolsNotInKit { get; set; } = [];
        public IEnumerable<KitDto>? KitsWithImplants { get; set; } = [];
        public IEnumerable<KitDto>? KitsWithoutImplants { get; set; } = [];
        public IEnumerable<UserDto>? Assistants { get; set; } = [];

    }
}
