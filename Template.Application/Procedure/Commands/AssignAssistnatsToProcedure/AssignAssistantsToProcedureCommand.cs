
using Template.Application.Abstraction.Commands;

namespace Template.Application.Procedure.Commands.AssignAssistnatsToProcedure
{
    public class AssignAssistantsToProcedureCommand : ICommand
    {
        public int ProcedureId { get; set; }
        public List<string> AssistantsIds { get; set; } = [];
    }
}
