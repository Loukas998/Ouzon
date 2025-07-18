using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQueryHandler(IProcedureRepository procedureRepository,
        IMapper mapper,
        ILogger<GetAllProceduresQueryHandler>logger) : IQueryHandler<GetAllProceduresQuery, IEnumerable<ProcedureDto>>
    {
        
        public async Task<Result<IEnumerable<ProcedureDto>>> Handle(GetAllProceduresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var procedures = await procedureRepository.GetAllAsync();
                if (procedures == null)
                {
                    return Result.Failure<IEnumerable<ProcedureDto>>(["Data not Found"]);
                }
                var res = mapper.Map<IEnumerable<ProcedureDto>>(procedures);
                return Result.Success(res);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Couldn't get procedures");
                return Result.Failure<IEnumerable<ProcedureDto>>(["Something went wrong"]);
            }
        }
    }
}
