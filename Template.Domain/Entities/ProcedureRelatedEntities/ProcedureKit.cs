using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
   public class ProcedureKit
    {
        public int Id { get; set; }
        public int KitId { get; set; }
        public int ProcedureId { get; set; }
    }
}
