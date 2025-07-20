using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Procedure.Queries.GetById
{
   public class GetProcedureQuery :IQuery<ProcedureDetailedDto>
    {
        public int Id { get; set; }
        public GetProcedureQuery(int id)
        {
            Id = id;
        }
    }
}
