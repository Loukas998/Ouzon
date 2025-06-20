using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
  public class ProcedureTool
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int ProcedureId { get; set; }
    }
}
