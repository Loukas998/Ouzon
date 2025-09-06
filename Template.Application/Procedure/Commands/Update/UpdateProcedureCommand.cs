using Template.Application.Abstraction.Commands;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Commands.Update;

public class UpdateProcedureCommand : ICommand
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public List<string>? AssistantIds { get; set; }
    public int CategoryId { get; set; }
    public EnumProcedureStatus? Status { get; set; }
    public List<int>? ToolIds { get; set; }
    public List<int>? KitIds { get; set; }

}
