using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Implants.Dtos;
using Template.Application.Procedure.Dtos;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.Materials;
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
                    SurgicalKits = result.Kits.Where(kit => kit.IsMainKit).ToList(),
                    ImplantKits = mapper.Map<List<ProcedureImplantToolsDetailsDto>>(result.Kits.Where(kit => kit.Implants.Any() && !kit.IsMainKit)),
                    RequiredTools = result.Tools.Where(tool => tool.KitId == null).ToList(),
                    Status = result.Status,
                    Doctor = result.Doctor,
                    Assistants = result.Assistants.ToList()??[]
                };
                var procedureImplantsWithTools = procedure.ProcedureImplantTools.GroupBy(pit => pit.Implant).Select(g => new ProcedureImplantToolsDetailsDto()
                {
                    Implant = mapper.Map<ImplantDto>(g.Key),
                    ToolsWithImplant = g.Where(pit => pit.Tool != null)
                        .Select(pit => mapper.Map<ToolDto>(pit.Tool)).Distinct()
                        .ToList()
                });
                detailedResult.ImplantKits.AddRange(procedureImplantsWithTools);

                var kitsWithoutImplants = result.Kits.Where(kit => !kit.Implants.Any() && !kit.IsMainKit).Select(kit => kit.Tools).ToList();
                detailedResult.RequiredTools.AddRange(mapper.Map<List<ToolDto>>(kitsWithoutImplants));

                var procedureImplants = procedure.ProcedureImplants.Where(pi => pi.Implant != null).Select(pi => mapper.Map<ImplantDto>(pi.Implant)).ToList();

                detailedResult.ImplantKits.AddRange(procedureImplants.Select(imp => new ProcedureImplantToolsDetailsDto()
                {
                    Implant = imp,
                    ToolsWithImplant = []
                }));
                
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
