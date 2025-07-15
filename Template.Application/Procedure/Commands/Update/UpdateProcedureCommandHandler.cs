using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.Update
{
    public class UpdateProcedureCommandHandler (IMapper mapper,IProcedureRepository procedureRepository,
        IAccountRepository accountRepository,IToolRepository toolRepository,IKitRepository kitRepository):
        ICommandHandler<UpdateProcedureCommand>
    {
        public async Task<Result> Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = await procedureRepository.GetDetailedWithId(request.Id);
            if(procedure == null)
            {
                return Result.Failure(["Entity not Found"]);
            }
            mapper.Map(request, procedure);
            if (request.AssistantIds != null)
            {

                if (procedure.AssistantsInProcedure == null)
                {
                    procedure.AssistantsInProcedure = new List<ProcedureAssistant>();
                }
                procedure.AssistantsInProcedure.Clear();
                foreach(var assistantId in request.AssistantIds)
                {
                    var exists = await accountRepository.UserInRoleAsync(assistantId, EnumRoleNames.AssistantDoctor.ToString());
                    if (!exists) 
                    {
                        return Result.Failure(["Assistant Doesn't Exist"]);
                    }
                    procedure.AssistantsInProcedure.Add(new ProcedureAssistant()
                    {
                        AsisstantId = assistantId
                    });
                }
            }
            if (request.ToolIds != null)
            {
                procedure.ToolsInProcedure.Clear();
                foreach(var tool in request.ToolIds)
                {
                    var toolEntity = await toolRepository.FindByIdAsync(tool);
                    if (toolEntity == null)
                    {
                        return Result.Failure(["Entity Not Found"]);
                    }
                    procedure.ToolsInProcedure.Add(new ProcedureTool()
                    {
                        ToolId = tool
                    });
                }
            }
            if (request.KitIds != null)
            {
                var mainKitCount = 0;
                procedure.KitsInProcedure.Clear();
                foreach (var kit in request.KitIds)
                {
                    var kitEntity = await kitRepository.FindByIdAsync(kit);
                    if (kitEntity == null)
                    {
                        return Result.Failure(["Entity Not Found"]);
                    }
                   else if (kitEntity.IsMainKit)
                    {
                        if (mainKitCount >= 1)
                        {
                            return Result.Failure(["There Can only be 1 Main Kit per Procedure"]);
                        }
                        mainKitCount++;
                    }
                    procedure.KitsInProcedure.Add(new ProcedureKit()
                    {
                        KitId = kit
                    });
                  
                }
            }
            await procedureRepository.UpdateAsync(procedure);
            return Result.Success();
        }
    }
}
