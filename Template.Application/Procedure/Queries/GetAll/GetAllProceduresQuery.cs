using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQuery :IQuery<IEnumerable<ProcedureDto>>
    {

    }
}
