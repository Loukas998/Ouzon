using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template.Application.Abstraction.Commands;
using Template.Application.Procedure.Dtos;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Procedure.Commands.Create;

public class CreateProcedureCommand : ICommand<int>
{
    [Range(0, 5, ErrorMessage = "Minimum amount of assistants is 0 Maximum amount is 5 ")]
    public int NumberOfAssistants { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public List<AddProcedureToolDto>? ToolsIds { get; set; }
    public List<int>? KitIds { get; set; }
    public List<ProcedureImplantToolsDto>?ImplantTools{ get; set; } 
    
}
