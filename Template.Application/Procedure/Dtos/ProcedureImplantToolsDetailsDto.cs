using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
