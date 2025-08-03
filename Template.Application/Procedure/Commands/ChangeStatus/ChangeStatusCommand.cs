using MediatR;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Commands.ChangeStatus;

public class ChangeStatusCommand : IRequest<ProcedureDetailedDto>
{
    public int ProcedureId { get; set; }
    public EnumProcedureStatus NewStatus { get; set; }
}
