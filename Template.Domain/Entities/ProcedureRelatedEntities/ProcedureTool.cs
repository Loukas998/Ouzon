using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Materials;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
  public class ProcedureTool
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
        public Tool? Tool { get; set; }
    }
}
