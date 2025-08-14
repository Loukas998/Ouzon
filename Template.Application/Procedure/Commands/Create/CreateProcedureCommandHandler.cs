using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.Create
{
    class CreateProcedureCommandHandler : ICommandHandler<CreateProcedureCommand, int>
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IToolRepository toolRepository;
        private readonly IKitRepository kitRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateProcedureCommandHandler> logger;
        private readonly IUserContext userContext;
        private readonly IImplantRepository implantRepository;
        private readonly INotificationService notificationService;
        public CreateProcedureCommandHandler(IProcedureRepository procedureRepository, IToolRepository toolRepository,
            IKitRepository kitRepository, ILogger<CreateProcedureCommandHandler> logger, IMapper mapper, IUserContext userContext,
            IImplantRepository implantRepository, INotificationService notificationService)
        {
            this.procedureRepository = procedureRepository;
            this.toolRepository = toolRepository;
            this.kitRepository = kitRepository;
            this.logger = logger;
            this.mapper = mapper;
            this.userContext = userContext;
            this.implantRepository = implantRepository;
            this.notificationService = notificationService;
        }
        public async Task<Result<int>> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var procedure = mapper.Map<Domain.Entities.ProcedureRelatedEntities.Procedure>(request);
                procedure.NumberOfAsisstants = request.NumberOfAssistants;
                var userId = userContext.GetCurrentUser();
                procedure.DoctorId = userId.Id;
                if (request.ToolsIds != null)
                {
                    foreach (var toolId in request.ToolsIds)
                    {
                        var tool = await toolRepository.FindByIdAsync(toolId.ToolId);
                        if (tool == null || tool.Quantity < toolId.Quantity)
                        {
                            return Result.Failure<int>(["Tool not found"]);
                        }
                        var proTool = new ProcedureTool()
                        {
                            ToolId = tool.Id,
                            Procedure = procedure,
                        };
                        tool.Quantity -= toolId.Quantity;
                        await toolRepository.UpdateAsync(tool);
                        procedure.ToolsInProcedure.Add(proTool);
                    }
                }
                if (request.KitIds != null)
                {
                    var mainKitCount = 0;
                    foreach (var kitId in request.KitIds)
                    {
                        var kit = await kitRepository.FindByIdAsync(kitId);
                        if (kit == null)
                        {
                            return Result.Failure<int>(["Kit not found"]);
                        }
                        else if (kit.IsMainKit)
                        {
                            if (mainKitCount >= 1)
                                return Result.Failure<int>(["There Can only be 1 Main Kit per Procedure"]);
                            mainKitCount++;
                        }
                        procedure.KitsInProcedure.Add(new ProcedureKit()
                        {
                            KitId = kit.Id
                        });
                    }
                }
                if (request.ImplantTools != null && request.ImplantTools.Count > 0)
                {
                    procedure.ProcedureImplants = new List<ProcedureImplant>();
                    procedure.ProcedureImplantTools = new List<ProcedureImplantTool>();

                    foreach (var ImplantTool in request.ImplantTools)
                    {
                        var implant = await implantRepository.FindByIdAsync(ImplantTool.ImplantId);
                        if (implant == null)
                        {
                            return Result.Failure<int>(["Implant Not Found"]);
                        }
                        if (ImplantTool.ToolIds == null || ImplantTool.ToolIds.Count == 0)
                        {
                            procedure.ProcedureImplants.Add(new ProcedureImplant()
                            {
                                ImplantId = implant.Id
                            });
                        }
                        else
                        {
                            foreach (var toolId in ImplantTool.ToolIds)
                            {
                                var tool = await toolRepository.FindByIdAsync(toolId);
                                if (tool == null)
                                {
                                    return Result.Failure<int>(["Tool with Implant Not Found"]);
                                }
                                procedure.ProcedureImplantTools.Add(new ProcedureImplantTool()
                                {
                                    ImplantId = implant.Id,
                                    ToolId = tool.Id
                                });

                            }
                        }
                    }
                }
                var procedureId = await procedureRepository.AddAsync(procedure);

                //sending notification to the admin
                var adminNotification = new Domain.Entities.Notifications.Notification
                {
                    Title = "New procedure",
                    Body = $"New procedure has been submitted by Dr.{userId.Email}",
                    Read = false,
                    DeviceId = null
                };
                await notificationService.SaveNotificationAsync(adminNotification);

                return Result.Success(procedureId.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not add procedure");
                return Result.Failure<int>([ex.Message, "Something Went Wrong"]);
            }
        }
    }
}
