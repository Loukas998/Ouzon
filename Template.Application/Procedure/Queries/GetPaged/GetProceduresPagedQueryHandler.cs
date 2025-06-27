using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetPaged
{
    public class GetProceduresPagedQueryHandler(IMapper mapper, IProcedureRepository procedureRepository,ILogger<GetProceduresPagedQueryHandler>logger) : IRequestHandler<GetProceduresPagedQuery, IEnumerable<ProcedureDto>>
    {
        public async Task<IEnumerable<ProcedureDto>> Handle(GetProceduresPagedQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting procedures: {@Implant}", request);
            var procedures = await procedureRepository.GetPagedResponseAsync(request.PageNum, request.PageSize);
            if(procedures == null)
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<ProcedureDto>>(procedures);
            return result;
        }
    }
}
