using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Application.Abstraction.Queries;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.AssignAssistnatsToProcedure;

public class AssignAssistantsToProcedureCommandHandler (IProcedureRepository procedureRepository
    ,IMapper mapper,IAccountRepository accountRepository): ICommandHandler<AssignAssistantsToProcedureCommand>
{
    public async Task<Result> Handle(AssignAssistantsToProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await procedureRepository.GetProcedureWithAssistants(request.ProcedureId);
        if (procedure == null)
        {
            return Result.Failure(["Entity not Found"]);
        }
        if (request.AssistantsIds.Count > procedure.NumberOfAsisstants)
        {
            return Result.Failure(["You are putting more Assistants than allowed"]);
        }
        procedure.AssistantsInProcedure.Clear();
        foreach (var assistantId in request.AssistantsIds)
        {
            var assistant = await accountRepository.GetUserAsync(assistantId);
            if (assistant == null) 
            {
                return Result.Failure(["Assistant not Found"]);
            }
            var roleCheck = await accountRepository.UserInRoleAsync(assistantId, "AssistantDoctor");
            if (!roleCheck)
            {
                return Result.Failure(["this User isn't an assistant"]);
            }
            procedure.AssistantsInProcedure.Add(new Domain.Entities.ProcedureRelatedEntities.ProcedureAssistant()
            {
                AsisstantId = assistantId,
            });
        }
        procedure.Status = Domain.Enums.EnumProcedureStatus.IN_PROGRESS;
        await procedureRepository.UpdateAsync(procedure);
        return Result.Success();
    }
}
