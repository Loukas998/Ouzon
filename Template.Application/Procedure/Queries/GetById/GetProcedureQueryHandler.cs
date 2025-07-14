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

namespace Template.Application.Procedure.Queries.GetById
{
    public class GetProcedureQueryHandler(IProcedureRepository procedureRepository, ILogger<GetProcedureQueryHandler> logger,IMapper mapper)
        : IQueryHandler<GetProcedureQuery, ProcedureDetailedDto>
    {
        public async Task<Result<ProcedureDetailedDto>> Handle(GetProcedureQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var procedure = await procedureRepository.GetDetailedWithId(request.Id);
                if (procedure == null)
                {
                    return Result.Failure<ProcedureDetailedDto>(["Couldn't find entity"]);
                }
                var result = mapper.Map<ProcedureDto>(procedure);
                var detailedResult = new ProcedureDetailedDto()
                {
                    Id = result.Id,
                    // AssistantId = result.AssistantId,
                    DoctorId = result.DoctorId,
                    CategoryId = result.CategoryId,
                    Date = result.Date,
                    KitsWithImplants = result.Kits.Where(kit => kit.Implants.Any()),
                    KitsWithoutImplants = result.Kits.Where(kit => !kit.Implants.Any()),
                    ToolsNotInKit = result.Tools.Where(tool => tool.KitId == null),
                    Status = result.Status,
                    Doctor = result.Doctor
                };
                return Result.Success(detailedResult);
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, "Something went wrong");
                throw;
            }
        }
    }
}
