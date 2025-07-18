using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos;

namespace Template.Application.Procedure.Queries.GetWithKitsOnly
{
   public class GetProcedureWithKitsOnlyQuery(int ProcedureId) :IQuery<ProcedureKitDetailsDto>
    {
        public int ProcedureId { get; set; }=ProcedureId;
    }
}
