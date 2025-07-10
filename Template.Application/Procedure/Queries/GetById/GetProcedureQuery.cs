using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos;

namespace Template.Application.Procedure.Queries.GetById
{
   public class GetProcedureQuery :IRequest<ProcedureDetailedDto>
    {
        public int Id { get; set; }
        public GetProcedureQuery(int id)
        {
            Id = id;
        }
    }
}
