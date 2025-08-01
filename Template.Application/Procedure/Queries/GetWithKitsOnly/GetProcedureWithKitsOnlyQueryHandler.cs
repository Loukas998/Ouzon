using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetWithKitsOnly;

public class GetProcedureWithKitsOnlyQueryHandler(IProcedureRepository procedureRepository,IMapper mapper) : IQueryHandler<GetProcedureWithKitsOnlyQuery, ProcedureKitDetailsDto>
{
    public async Task<Result<ProcedureKitDetailsDto>> Handle(GetProcedureWithKitsOnlyQuery request, CancellationToken cancellationToken)
    {
        var procedure = await procedureRepository.GetProcedureWithKits(request.ProcedureId);
        if(procedure == null)
        {
            return Result.Failure<ProcedureKitDetailsDto>(["Entity not Found"]);
        }
        var procDto = mapper.Map<ProcedureDto>(procedure);
        var procKits = new ProcedureKitDetailsDto()
        {
            MainKits = procDto.Kits.Where(k => k.IsMainKit),
            KitsWithImplants = procDto.Kits.Where(k => !k.IsMainKit && k.Implants.Any()),
            KitsWithoutImplants = procDto.Kits.Where(k => !k.IsMainKit && !k.Implants.Any()),
        };
        return Result.Success(procKits);
    }
}
