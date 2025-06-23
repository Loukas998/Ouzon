using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.Create
{
    class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand, int>
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
        public async Task<int> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var procedure = mapper.Map<Domain.Entities.ProcedureRelatedEntities.Procedure>(request);
                procedure.DoctorId = userContext.GetCurrentUser().Id;
                foreach (var toolId in request.ToolsIds)
                {
                    var tool = await toolRepository.FindByIdAsync(toolId);
                    if (tool == null)
                    {
                        throw new Exception("tool not found");
                    }
                    procedure.ToolsInProcedure.Add(new ProcedureTool()
                    {
                        ToolId = tool.Id
                    });
                }
                foreach (var kitId in request.KitIds)
                {
                    var tool = await kitRepository.FindByIdAsync(kitId);
                    if (tool == null)
                    {
                        throw new Exception();
                    }
                    procedure.KitsInProcedure.Add(new ProcedureKit()
                    {
                        KitId = tool.Id
                    });
                }
                var procedureId = await procedureRepository.AddAsync(procedure);
                return procedureId.Id;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Could not add procedure");
                return -1;
            }
        }
    }
}
