using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetPaged
{
    public class GetProceduresPagedQueryHandler(IMapper mapper, IProcedureRepository procedureRepository,
        ILogger<GetProceduresPagedQueryHandler>logger, IUserContext userContext) : IRequestHandler<GetProceduresPagedQuery, IEnumerable<ProcedureDto>>
    {
        public async Task<IEnumerable<ProcedureDto>> Handle(GetProceduresPagedQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Getting procedures: {@Procedure}", request);
            var procedures = await procedureRepository.GetFilteredProcedures(request.PageSize, request.PageNum,request.DoctorId,request.AssistantId);
            if(procedures == null)
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<ProcedureDto>>(procedures);
            return result;
        }
    }
}
