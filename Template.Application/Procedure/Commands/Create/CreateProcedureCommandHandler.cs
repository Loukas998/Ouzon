using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.Create
{
    class CreateProcedureCommandHandler : ICommandHandler<CreateProcedureCommand,int>
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IToolRepository toolRepository;
        private readonly IKitRepository kitRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateProcedureCommandHandler> logger;
        private readonly IUserContext userContext;
        public CreateProcedureCommandHandler(IProcedureRepository procedureRepository, IToolRepository toolRepository, 
            IKitRepository kitRepository, ILogger<CreateProcedureCommandHandler> logger, IMapper mapper, IUserContext userContext)
        {
            this.procedureRepository = procedureRepository;
            this.toolRepository = toolRepository;
            this.kitRepository = kitRepository;
            this.logger = logger;
            this.mapper = mapper;
            this.userContext = userContext;
        }
        public async Task<Result<int>> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var procedure = mapper.Map<Domain.Entities.ProcedureRelatedEntities.Procedure>(request);
                procedure.DoctorId = userContext.GetCurrentUser().Id;
                if (request.ToolsIds != null)
                {
                    foreach (var toolId in request.ToolsIds)
                    {
                        var tool = await toolRepository.FindByIdAsync(toolId);
                        if (tool == null)
                        {
                            return Result.Failure<int>(["Tool not found"]);
                        }
                        var proTool = new ProcedureTool()
                        {
                            ToolId = tool.Id,
                            Procedure = procedure,
                        };
                        procedure.ToolsInProcedure.Add(proTool);
                    }
                }
                if (request.KitIds != null)
                {
                    foreach (var kitId in request.KitIds)
                    {
                        var kit = await kitRepository.FindByIdAsync(kitId);
                        if (kit == null)
                        {
                            return Result.Failure<int>(["Tool not found"]);
                        }
                        procedure.KitsInProcedure.Add(new ProcedureKit()
                        {
                            KitId = kit.Id
                        });
                    }
                }

                var procedureId = await procedureRepository.AddAsync(procedure);
                return Result.Success(procedureId.Id);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Could not add procedure");
                return Result.Failure<int>([ex.Message,"Something Went Wrong"]);
            }
        }
    }
}
