using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Commands.Update;

public class UpdateProcedureCommand :IRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string AssistantId { get; set; }
    public int CategoryId { get; set; }
    public EnumProcedureStatus Status { get; set; }
}
