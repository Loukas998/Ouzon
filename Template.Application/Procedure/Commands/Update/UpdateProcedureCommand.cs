using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Commands.Update;

public class UpdateProcedureCommand : ICommand
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public List<string>? AssistantIds { get; set; }
    public int? CategoryId { get; set; }
    public EnumProcedureStatus? Status { get; set; }
    public List<int>? ToolIds { get; set; }
    public List<int>? KitIds { get; set; }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }
}
