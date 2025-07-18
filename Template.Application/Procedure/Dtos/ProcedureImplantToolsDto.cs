using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Procedure.Dtos
{
    public class ProcedureImplantToolsDto
    {
        public int ImplantId { get; set; }
        public List<int>? ToolIds { get; set; }
    }
}
