using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQuery :IRequest<IEnumerable<ProcedureDto>>
    {

    }
}
