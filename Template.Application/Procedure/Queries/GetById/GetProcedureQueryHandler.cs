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

namespace Template.Application.Procedure.Queries.GetById
{
    public class GetProcedureQueryHandler(IProcedureRepository procedureRepository, ILogger<GetProcedureQueryHandler> logger,IMapper mapper) : IRequestHandler<GetProcedureQuery, ProcedureDetailedDto>
    {
        public async Task<ProcedureDetailedDto> Handle(GetProcedureQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var procedure = await procedureRepository.GetWithToolsAndKitsAsync(request.Id);
                if (procedure == null)
                {
                    throw new Exception();
                }
                var result = mapper.Map<ProcedureDto>(procedure);
                var detailedResult = new ProcedureDetailedDto()
                {
                    Id = result.Id,
                    AssistantId = result.AssistantId,
                    DoctorId = result.DoctorId,
                    CategoryId = result.CategoryId,
                    Date = result.Date,
                    KitsWithImplants = result.Kits.Where(kit => kit.Implants.Any()),
                    KitsWithoutImplants = result.Kits.Where(kit => !kit.Implants.Any()),
                    ToolsNotInKit = result.Tools.Where(tool => tool.KitId == null),
                    Status = result.Status,
                };
                return detailedResult;
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, "Something went wrong");
                throw;
            }
        }
    }
}
