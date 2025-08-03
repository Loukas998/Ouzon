using AutoMapper;
using MediatR;
using Template.Application.Implants.Dtos;
using Template.Application.Procedure.Dtos;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Tools.Dtos;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.ChangeStatus;

public class ChangeStatusCommandHandler(IProcedureRepository procedureRepository,
    IMapper mapper) : IRequestHandler<ChangeStatusCommand, ProcedureDetailedDto>
{
    public async Task<ProcedureDetailedDto> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var procedure = await procedureRepository.GetDetailedWithId(request.ProcedureId);
        if (procedure == null)
        {
            throw new NotFoundException(nameof(Procedure), request.ProcedureId.ToString());
        }

        procedure.Status = request.NewStatus;
        await procedureRepository.SaveChangesAsync();
        var result = mapper.Map<ProcedureDto>(procedure);
        var detailedResult = new ProcedureDetailedDto()
        {
            Id = result.Id,
            DoctorId = result.DoctorId,
            CategoryId = result.CategoryId,
            Date = result.Date,
            SurgicalKits = result.Kits.Where(kit => kit.IsMainKit).ToList(),
            ImplantKits = mapper.Map<List<ProcedureImplantToolsDetailsDto>>(result.Kits.Where(kit => kit.Implants.Any() && !kit.IsMainKit)),
            RequiredTools = result.Tools.ToList(),
            Status = result.Status,
            Doctor = result.Doctor,
            Assistants = result.Assistants.ToList() ?? []
        };
        var procedureImplantsWithTools = procedure.ProcedureImplantTools.GroupBy(pit => pit.Implant).Select(g => new ProcedureImplantToolsDetailsDto()
        {
            Implant = mapper.Map<ImplantDto>(g.Key),
            ToolsWithImplant = g.Where(pit => pit.Tool != null)
                .Select(pit => mapper.Map<ToolDto>(pit.Tool)).Distinct()
                .ToList()
        });
        detailedResult.ImplantKits.AddRange(procedureImplantsWithTools);

        var kitsWithoutImplants = result.Kits.Where(kit => !kit.Implants.Any() && !kit.IsMainKit).SelectMany(k => k.Tools);
        detailedResult.RequiredTools.AddRange(mapper.Map<List<ToolDto>>(kitsWithoutImplants));

        var procedureImplants = procedure.ProcedureImplants.Where(pi => pi.Implant != null).Select(pi => mapper.Map<ImplantDto>(pi.Implant)).ToList();

        detailedResult.ImplantKits.AddRange(procedureImplants.Select(imp => new ProcedureImplantToolsDetailsDto()
        {
            Implant = imp,
            ToolsWithImplant = []
        }));

        return detailedResult;
    }
}
