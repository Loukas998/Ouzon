using Template.Application.Implants.Dtos;
using Template.Application.Tools.Dtos;

namespace Template.Application.Procedure.Dtos
{
    public class ProcedureImplantToolsDetailsDto
    {
        public ImplantDto Implant { get; set; }
        public List<ToolDto> ToolsWithImplant { get; set; }

    }
}
