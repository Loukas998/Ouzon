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

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQueryHandler(IProcedureRepository procedureRepository,
        IMapper mapper,
        ILogger<GetAllProceduresQueryHandler>logger) : IRequestHandler<GetAllProceduresQuery, IEnumerable<ProcedureDto>>
    {
        
        public async Task<IEnumerable<ProcedureDto>> Handle(GetAllProceduresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var procedures = await procedureRepository.GetAllAsync();
                if (procedures == null)
                {
                    return new List<ProcedureDto>();
                }
                var result = mapper.Map<IEnumerable<ProcedureDto>>(procedures);
                return result;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Couldn't get procedures");
                throw;
            }
        }
    }
}
