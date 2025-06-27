using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Procedure.Commands.Create;

public class CreateProcedureCommand :IRequest<int>
{
    public bool HasAssistant { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public List<int>? ToolsIds { get; set; }
    public List<int>? KitIds { get; set; }
    
}
