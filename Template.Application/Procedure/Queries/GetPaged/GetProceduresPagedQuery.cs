using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos;

namespace Template.Application.Procedure.Queries.GetPaged
{
   public class GetProceduresPagedQuery(int pageSize,int pageNum, string? DoctorId, string? AssistantId):IRequest<IEnumerable<ProcedureDto>>
    {
        public int PageSize { get; set; } = pageSize;
        public int PageNum { get; set; } = pageNum;
        public string? DoctorId { get; set; } = DoctorId != null ? DoctorId : null;
        public string? AssistantId { get; set; } = AssistantId != null ? AssistantId: null;

    }
}
